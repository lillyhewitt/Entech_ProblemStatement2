using Data.Database;
using Data.Tables;
using Data.Validations;
using Microsoft.AspNetCore.Http;
using Services.Interfaces.ApplicationsDisbursed;
using Services.Responses.ApplicationsDisbursed;

namespace Services.Services.ApplicationsDisbursed
{
    public sealed class ApplicationsDisbursedAPRService : ApplicationsDisbursedBaseService, IApplicationsDisbursedAPRService
    {
        // make database instance 
        public ApplicationsDisbursedAPRService(
            AppContext dbContext,
            IHttpContextAccessor httpContextAccessor,
            DisbursementStateIdsValidation disbursementStateIds)
            : base(dbContext, httpContextAccessor, disbursementStateIds) { }

        // query of disbursed loans that takes out entries with the wrong client id and/or invalid apr's
        protected IQueryable<ApplicationPrice> AppsDisbursedQuery(int clientId)
        {
            // query to find distinct applicationIds
            var query = _dbContext.ApplicationPrices
                        .Where(applicationPrice => applicationPrice.ClientId == clientId && applicationPrice.Apr > 0.0m)
                        .Select(applicationPrice => applicationPrice);

            return query;
        }

        // groups values by applicationId and select the first value associated with applicationId
        protected List<decimal> GroupByApplicationId(IQueryable<ApplicationPrice> query)
        {
            // group by ApplicationId and select the first APR in each group
            var groupedAPRs = query.GroupBy(applicationPrice => applicationPrice.ApplicationId)
                                     .Select(group => group.First().Apr ?? 0);

            return groupedAPRs.ToList();
        }

        // list the mean and median APRs for disbursed loans
        public ApplicationsDisbursedAPRResponse CreateMeanAndMedianResponse(
            int clientId,
            decimal? startRange = null,
            decimal? endRange = null,
            DateTime? startDate = null,
            DateTime? endDate = null)
        {
            // query finding entries with matching client ids and valid credit scores
            var query = AppsDisbursedQuery(clientId);

            // filter query apr ranges
            var filteredAPRs = FilterByRanges(query, startRange, endRange, ap => ap.Apr);

            // filters by dates
            filteredAPRs = FilterByDates(filteredAPRs, startDate, endDate, ap => ap.CreatedAt);

            // group APRs by application ID
            var groupedAPRs = GroupByApplicationId(filteredAPRs);

            // get the average and median APRs from queries
            decimal average = groupedAPRs.Average();
            decimal median = CalculateMedian(groupedAPRs);

            // create a response of the results (mean and median APRs for specified client)
            return new ApplicationsDisbursedAPRResponse
            {
                Average = Math.Round(average, 4),
                Median = Math.Round(median, 4)
            };
        }
    }
}
