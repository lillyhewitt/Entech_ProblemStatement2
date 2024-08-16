using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Data.Database;
using Services.Interfaces.ApplicationsDisbursed;
using Services.Responses.Applications;

namespace Services.Services.Applications
{
    public sealed class ApplicationsTotalService : ApplicationsBaseService, IApplicationsTotalService
    {
        // create instance of AppContext (database)
        public ApplicationsTotalService(AppContext dbContext, IHttpContextAccessor httpContextAccessor) : base(dbContext, httpContextAccessor) { }

        // Count the number of loans 
        protected int QueryAppsTotal(int clientId, DateTime? startDate, DateTime? endDate)
        {
            // query to get the number of loans per specific applicant
            var queryTotal = _dbContext.Applicants
                             .Include(applicant => applicant.Applications)
                             .AsQueryable();

            // filters by dates
            var filteredQuery = FilterByDates(queryTotal, startDate, endDate, applicant => applicant.CreatedAt);

            // return the results (number of loans)
            return filteredQuery.Count(applicant => applicant.ClientId == clientId);
        }

        // create response of the number of loans
        public ApplicationsTotalResponse CreateAppsTotalResponse(int clientId, DateTime? startDate = null, DateTime? endDate = null)
        {
            // run the query
            var queryResult = QueryAppsTotal(clientId, startDate, endDate);

            // create a response of the query results (number of loans for specified client)
            return new ApplicationsTotalResponse
            {
                Loans = queryResult
            };
        }
    }
}
