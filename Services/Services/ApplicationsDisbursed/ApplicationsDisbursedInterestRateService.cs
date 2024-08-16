using Services.Interfaces.ApplicationsDisbursed;
using Services.Responses.ApplicationsDisbursed;
using Microsoft.AspNetCore.Http;
using Data.Database;
using Data.Validations;
using Data.Tables;

namespace Services.Services.ApplicationsDisbursed
{
    public sealed class ApplicationsDisbursedInterestRateService : ApplicationsDisbursedBaseService, IApplicationsDisbursedInterestRateService
    {
        // make database instance 
        public ApplicationsDisbursedInterestRateService(
            AppContext dbContext,
            IHttpContextAccessor httpContextAccessor,
            DisbursementStateIdsValidation disbursementStateIds)
            : base(dbContext, httpContextAccessor, disbursementStateIds) { }

        // query of all loans that takes out entries with the wrong client id and/or interest rates
        protected IQueryable<ApplicationPrice> AppsDisbursedQuery(int clientId)
        {
            // find the mean credit score for disbursed loans
            var query = _dbContext.ApplicationPrices
                      .Where(applicationPrice => applicationPrice.ClientId == clientId && applicationPrice.CalculatedRate > 0.0m)
                      .Select(applicationPrice => applicationPrice);

            return query;
        }

        // groups values by applicationId and select the first value associated with applicationId
        protected List<decimal> GroupByApplicationId(IQueryable<ApplicationPrice> query)
        {
            // group by ApplicationId and select the first interest rate in each group
            var groupedRates = query.GroupBy(applicationPrice => applicationPrice.ApplicationId)
                                     .Select(group => group.First().CalculatedRate ?? 0);

            return groupedRates.ToList();
        }


        // lists the mean and median of the interest rates
        public ApplicationsDisbursedInterestRateResponse CreateMeanAndMedianResponse(
            int clientId,
            decimal? startRange = null,
            decimal? endRange = null,
            DateTime? startDate = null,
            DateTime? endDate = null)
        {
            // query finding entries with matching client ids and valid credit scores
            var query = AppsDisbursedQuery(clientId);

            // filter query by interest rate ranges
            var filteredRates = FilterByRanges(query, startRange, endRange, ap => ap.CalculatedRate);

            // filters by dates
            filteredRates = FilterByDates(filteredRates, startDate, endDate, ap => ap.CreatedAt);

            // group interest rates by application ID
            var groupedRates = GroupByApplicationId(filteredRates);

            // get the average and median interest rates from the queries
            decimal average = groupedRates.Average();
            decimal median = CalculateMedian(groupedRates);

            // create a response from the query results (mean and median interest rates for specified client)
            return new ApplicationsDisbursedInterestRateResponse
            {
                Average = Math.Round(average, 4),
                Median = Math.Round(median, 4)
            };
        }
    }
}
