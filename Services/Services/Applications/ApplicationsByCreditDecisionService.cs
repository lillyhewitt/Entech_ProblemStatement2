using Microsoft.AspNetCore.Http;
using Services.Interfaces.Applications;
using Services.QueryResults.Applications;
using Services.Responses.Applications;
using Data.Database;

namespace Services.Services.Applications
{
    public sealed class ApplicationsByCreditDecisionService : ApplicationsBaseService, IApplicationsByCreditDecisionService
    {
        // create instance of AppContext (database)
        public ApplicationsByCreditDecisionService(
            AppContext dbContext, 
            IHttpContextAccessor httpContextAccessor) 
            : base(dbContext, httpContextAccessor) { }

        // Count the number of applications per credit decision of a specific applicant
        public List<ApplicationsByCreditDecisionsResult> QueryAppsByCreditDecision(
            int clientId,
            long? applicationId,
            DateTime? startDate,
            DateTime? endDate)
        {
            // initial query to filter by clientId
            var queryFiltered = _dbContext.CreditDecisions
                                .Where(creditDecision => creditDecision.ClientId == clientId);

            // filters by applicationId
            var filteredQuery = FilterByApplicationId(queryFiltered, applicationId, item => item.ApplicationId);

            // filters by dates
            filteredQuery = FilterByDates(filteredQuery, startDate, endDate, item => item.CreatedAt);

            // group by ProviderDecisionDescription and select the results
            var groupedQuery = filteredQuery
                            .GroupBy(cd => cd.ProviderDecisionDescription)
                            .Select(grouping => new ApplicationsByCreditDecisionsResult
                            {
                                ProviderDecisionDescription = grouping.Key,
                                TotalNum = grouping.Select(cd => cd.ApplicationId).Distinct().Count()
                            });

            // return the results of the query
            return groupedQuery.ToList();
        }

        // create list of repsonses for number of applications per credit decision
        public List<ApplicationsByCreditDecisionResponse> CreateAppsByCreditDecisionResponses(
            int clientId,
            long? applicationId = null,
            DateTime? startDate = null,
            DateTime? endDate = null)
        {
            // run the query
            var queryResult = QueryAppsByCreditDecision(clientId, applicationId, startDate, endDate);

            // create list of responses
            var creditDecisionsResponseList = queryResult
                .Select(item => new ApplicationsByCreditDecisionResponse
                {
                    CreditDecision = item.ProviderDecisionDescription,
                    Number = item.TotalNum
                })
                .ToList();

            return creditDecisionsResponseList;
        }

        // create list of repsonses for the credit decision of a specific application
        public List<ApplicationByCreditDecisionResponse> CreateAppByCreditDecisionResponses(
            int clientId,
            long? applicationId = null,
            DateTime? startDate = null,
            DateTime? endDate = null)
        {
            // run the query
            var queryResult = QueryAppsByCreditDecision(clientId, applicationId, startDate, endDate);

            // create list of responses
            var creditDecisionsResponseList = queryResult
                .Select(item => new ApplicationByCreditDecisionResponse
                {
                    CreditDecision = item.ProviderDecisionDescription
                })
                .ToList();

            return creditDecisionsResponseList;
        }
    }
}
