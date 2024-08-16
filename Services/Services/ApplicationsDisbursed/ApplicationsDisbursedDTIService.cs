using Services.Interfaces.ApplicationsDisbursed;
using Services.Responses.ApplicationsDisbursed;
using Microsoft.AspNetCore.Http;
using Data.Database;
using Data.Tables;
using Data.Validations;

namespace Services.Services.ApplicationsDisbursed
{
    public sealed class ApplicationsDisbursedDTIService : ApplicationsDisbursedBaseService, IApplicationsDisbursedDTIService
    {
        // make database instance 
        public ApplicationsDisbursedDTIService(
            AppContext dbContext,
            IHttpContextAccessor httpContextAccessor,
            DisbursementStateIdsValidation disbursementStateIds)
            : base(dbContext, httpContextAccessor, disbursementStateIds) { }

        // query of disbursed loans that takes out entries with the wrong client id and/or dti's
        protected IQueryable<ApplicationPrice> AppsDisbursedQuery(int clientId)
        {
            // find the mean credit score for disbursed loans
            var query = from ac in _dbContext.ApplicationConsolidations
                        join dsm in _dbContext.DisbursementStateManagements
                           on ac.ApplicationDisbursementId equals dsm.Id
                        join ap in _dbContext.ApplicationPrices
                           on ac.ApplicationId equals ap.ApplicationId
                        join ad in _dbContext.ApplicationDisbursements
                           on ap.ApplicationId equals ad.ApplicationId
                        where RetrieveDisbursementStateIds().Contains(dsm.DisbursementStateId)
                              && ac.ClientId == clientId
                              && ad.DisbursedDate != null
                              && ap.Dti > 0
                        select ap;

            return query;
        }

        // groups values by applicationId and select the first value associated with applicationId
        protected List<decimal> GroupByApplicationId(IQueryable<ApplicationPrice> query)
        {
            // group by ApplicationId and select the first Dti in each group
            var groupedDTIs = query.GroupBy(applicationPrice => applicationPrice.ApplicationId)
                                     .Select(group => group.First().Dti ?? 0);

            return groupedDTIs.ToList();
        }


        // lists the average DTI for disbursed loans
        public ApplicationsDisbursedDTIResponse CreateMeanAndMedianResponse(
            int clientId,
            decimal? startRange = null,
            decimal? endRange = null,
            DateTime? startDate = null,
            DateTime? endDate = null)
        {
            // query finding entries with matching client ids and valid credit scores
            var query = AppsDisbursedQuery(clientId);

            // filter query by dti ranges
            var filteredDTIs = FilterByRanges(query, startRange, endRange, ap => ap.Dti);

            // filters by dates
            filteredDTIs = FilterByDates(filteredDTIs, startDate, endDate, ap => ap.CreatedAt);

            // group DTIs by application ID
            var groupedDTIs = GroupByApplicationId(filteredDTIs);

            // get the average and median DTI from queries
            decimal average = groupedDTIs.Average();
            decimal median = CalculateMedian(groupedDTIs);

            // create a response from the query results (average and median DTI for specified client)
            return new ApplicationsDisbursedDTIResponse
            {
                Average = Math.Round(average, 4),
                Median = Math.Round(median, 4)
            };
        }
    }
}
