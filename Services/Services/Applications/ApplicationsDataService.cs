using Microsoft.AspNetCore.Http;
using Services.Interfaces.Applications;
using Services.QueryResults.Applications;
using Services.Responses.Applications;
using Data.Database;

namespace Services.Services.Applications
{
    public sealed class ApplicationsDataService : ApplicationsBaseService, IApplicationsDataService
    {
        // create instance of AppContext (database)
        public ApplicationsDataService(AppContext dbContext, IHttpContextAccessor httpContextAccessor) : base(dbContext, httpContextAccessor) { }

        // collect data on all loans with a specific applicationId or for all applications
        protected List<ApplicationsDataResult> QueryAppsData(int clientId, long? applicationId, DateTime? startDate, DateTime? endDate)
        {
            // query to get data on each loan/application of a specific applicant
            var queryLoanData = _dbContext.Applicants
                             .SelectMany(applicant => applicant.Applications, (applicant, application) => new { Applicant = applicant, Application = application })
                             .Where(applicant => applicant.Application.ClientId == clientId)
                             .Select(grouping => new ApplicationsDataResult
                             {
                                 ApplicationId = grouping.Application.Id,
                                 DateOfBirth = grouping.Applicant.DateOfBirth,
                                 CreatedAt = grouping.Applicant.CreatedAt
                             });

            // filters by applicationId
            var filteredQuery = FilterByApplicationId(queryLoanData, applicationId, item => item.ApplicationId);

            // filters by dates
            filteredQuery = FilterByDates(filteredQuery, startDate, endDate, item => item.CreatedAt);

            // return the results of the query
            return filteredQuery.ToList();
        }

        // create a list of responses with data on each loan/application
        public List<ApplicationsDataResponse> CreateAppsDataResponse(int clientId, long? applicationId = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            // run the query
            var queryOfLoanData = QueryAppsData(clientId, applicationId, startDate, endDate);

            // create list of responses of the query results (data per loan)
            var loansList = queryOfLoanData
              .Select(item => new ApplicationsDataResponse
              {
                  ApplicationId = item.ApplicationId,
                  DateOfBirth = item.DateOfBirth,
                  CreatedAt = item.CreatedAt
              })
              .ToList();

            return loansList;
        }
    }
}
