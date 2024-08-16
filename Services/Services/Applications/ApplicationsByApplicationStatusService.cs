using Microsoft.AspNetCore.Http;
using Services.Interfaces.Applications;
using Services.QueryResults.Applications;
using Services.Responses.Applications;
using Data.Database;

namespace Services.Services.Applications
{
    public sealed class ApplicationsByApplicationStatusService : ApplicationsBaseService, IApplicationsByApplicationStatusService
    {
        // make database instance 
        public ApplicationsByApplicationStatusService(AppContext dbContext, IHttpContextAccessor httpContextAccessor) : base(dbContext, httpContextAccessor) { }

        // counts the number of loans per application status per specific applicant 
        public List<ApplicationsByApplicationStatusResult> QueryAppsByApplicationStatus(
            int clientId,
            long? applicationId,
            DateTime? startDate,
            DateTime? endDate)
        {
            // initial query to filter by clientId
            var queryFiltered = _dbContext.ProcessStates
                                .Join(_dbContext.ApplicationStateManagements,
                                    processState => processState.Id,
                                    applicationStateManagement => applicationStateManagement.ApplicationStateId,
                                    (ps, asm) => new
                                    {
                                        ps.StatusName,
                                        asm.Id,
                                        asm.ClientId,
                                        asm.CreatedAt
                                    })
                                .Where(grouping => grouping.ClientId == clientId);

            // filters by applicationId
            var filteredQuery = FilterByApplicationId(queryFiltered, applicationId, item => item.Id);

            // filters by dates
            filteredQuery = FilterByDates(filteredQuery, startDate, endDate, item => item.CreatedAt);

            // group by StatusName and select the results
            var groupedQuery = filteredQuery
                            .GroupBy(grouping => grouping.StatusName)
                            .Select(grouping => new ApplicationsByApplicationStatusResult
                            {
                                StatusName = grouping.Key,
                                TotalNum = grouping.Select(x => x.Id).Distinct().Count()
                            });

            // return the results from the query
            return groupedQuery.ToList();
        }

        // create list of repsonses for number of applications per application status
        public List<ApplicationsByApplicationStatusResponse> CreateAppsByApplicationStatusResponses(
            int clientId,
            long? applicationId = null,
            DateTime? startDate = null,
            DateTime? endDate = null)
        {
            // run the query
            var queryResult = QueryAppsByApplicationStatus(clientId, applicationId, startDate, endDate);

            // create a list of responses
            var appStatusResponseList = queryResult
                .Select(item => new ApplicationsByApplicationStatusResponse
                {
                    ApplicationStatus = item.StatusName,
                    Number = item.TotalNum
                })
                .ToList();

            return appStatusResponseList;
        }

        // create list of repsonses for number application statuses of a specific applicant
        public List<ApplicationByApplicationStatusResponse> CreateAppByApplicationStatusResponses(
            int clientId,
            long? applicationId = null,
            DateTime? startDate = null,
            DateTime? endDate = null)
        {
            // run the query
            var queryResult = QueryAppsByApplicationStatus(clientId, applicationId, startDate, endDate);

            // create a list of responses
            var appStatusResponseList = queryResult
                .Select(item => new ApplicationByApplicationStatusResponse
                {
                    ApplicationStatus = item.StatusName
                })
                .ToList();

            return appStatusResponseList;
        }
    }
}
