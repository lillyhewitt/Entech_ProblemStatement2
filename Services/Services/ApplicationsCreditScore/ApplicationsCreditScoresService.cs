using Services.Interfaces.CreditScore;
using Services.Responses.Applications;
using Services.Responses.CreditScore;
using Services.Services.ApplicationsDisbursed;
using Microsoft.AspNetCore.Http;
using Data.Database;
using Data.Validations;
using Data.Tables;

namespace Services.Services.ApplicationsCreditScore
{
    public sealed class ApplicationsCreditScoresService : ApplicationsDisbursedBaseService, IApplicationsCreditScoresService
    {
        // create variables to store average and median
        private decimal average;
        private decimal median;

        // make database instance 
        public ApplicationsCreditScoresService(
            AppContext dbContext,
            IHttpContextAccessor httpContextAccessor,
            DisbursementStateIdsValidation disbursementStateId)
            : base(dbContext, httpContextAccessor, disbursementStateId) { }

        // query of all loans that takes out entries with the wrong client id and/or invalid credit scores
        protected IQueryable<CreditResponse> AppsQuery(int clientId)
        {
            // query to filter by clientId and valid credit scores
            var query = _dbContext.CreditResponses
                        .Where(cr => cr.ClientId == clientId && cr.CreditScore != 4 && cr.CreditScore != 0 && cr.CreditScore != null);

            return query;
        }

        // query of disbursed loans that takes out entries with the wrong client id and/or invalid credit scores
        protected IQueryable<CreditResponse> AppsDisbursedQuery(int clientId)
        {
            // initial query to find the credit scores for disbursed loans and filter out clientId and invalid credit scores
            var query = from ac in _dbContext.ApplicationConsolidations
                        join dsm in _dbContext.DisbursementStateManagements
                        on ac.ApplicationDisbursementId equals dsm.Id
                        join cr in _dbContext.CreditResponses
                        on ac.ApplicationId equals cr.ApplicationId
                        join ad in _dbContext.ApplicationDisbursements
                        on cr.ApplicationId equals ad.ApplicationId
                        where RetrieveDisbursementStateIds().Contains(dsm.DisbursementStateId)
                              && cr.ClientId == clientId
                              && ad.DisbursedDate != null
                              && cr.CreditScore > 0
                              && cr.CreditScore != 4
                        select cr;

            return query;
        }

        // group credit scores by applicationId
        protected List<decimal> GroupScoresByApplicationId(IQueryable<CreditResponse> query)
        {
            // group by ApplicationId and select the first credit score in each group
            var groupedScores = query.GroupBy(creditResponse => creditResponse.ApplicationId)
                         .Select(group => group.Select(cr => (decimal?)cr.CreditScore).FirstOrDefault() ?? 0m);

            return groupedScores.ToList();
        }

        // queries data then finds the mean and media credit scores for all or disbursed loans
        public void CollectMeanAndMedian(
            IQueryable<CreditResponse> query,
            decimal? startRange = null,
            decimal? endRange = null,
            DateTime? startDate = null,
            DateTime? endDate = null)
        {
            // filter query by credit score ranges
            var filteredScores = FilterByRanges(query, startRange, endRange, cr => cr.CreditScore);

            // filters by dates
            filteredScores = FilterByDates(filteredScores, startDate, endDate, cr => cr.CreatedAt);

            // group APRs by application ID
            var groupedScores = GroupScoresByApplicationId(filteredScores);

            // get the average and median credit score from queries
            average = groupedScores.Average(); ;
            median = CalculateMedian(groupedScores);
        }

        // lists the mean and media credit scores for all loans
        public ApplicationsCreditScoresResponse CreateMeanAndMedianAppsResponse(
            int clientId,
            decimal? startRange = null,
            decimal? endRange = null,
            DateTime? startDate = null,
            DateTime? endDate = null)
        {
            // query finding entries with matching client ids and valid credit scores
            var query = AppsQuery(clientId);

            // calculate mean and median and update private variables
            CollectMeanAndMedian(query, startRange, endRange, startDate, endDate);

            // return the results (mean and median credit scores)
            return new ApplicationsCreditScoresResponse
            {
                Average = Math.Round(average, 2),
                Median = Math.Round(median, 2)
            };
        }

        // lists the mean and media credit scores for disbursed loans
        public ApplicationsAveragesResponse CreateMeanAndMedianAppsDisbursedResponse(
            int clientId,
            decimal? startRange = null,
            decimal? endRange = null,
            DateTime? startDate = null,
            DateTime? endDate = null)
        {
            // query finding entries with matching client ids and valid credit scores
            var query = AppsDisbursedQuery(clientId);

            // calculate mean and median and update private variables
            CollectMeanAndMedian(query, startRange, endRange, startDate, endDate);

            // return the results (mean and median credit scores)
            return new ApplicationsAveragesResponse
            {
                Average = Math.Round(average, 2),
                Median = Math.Round(median, 2)
            };
        }
    }
}
