using Services.Interfaces.ApplicationsDisbursed;
using Services.Responses.Applications;
using Services.Services.Applications;
using Microsoft.AspNetCore.Http;
using Data.Database;

namespace Services.Services.Applicants
{
    public sealed class ApplicationsTotalByApplicantTypeService : ApplicationsBaseService, IApplicationsTotalByApplicantTypeService
    {
        // create instance of AppContext (database)
        public ApplicationsTotalByApplicantTypeService(
            AppContext dbContext,
            IHttpContextAccessor httpContextAccessor)
            : base(dbContext, httpContextAccessor) { }

        // Count the number of applications per applicant type
        protected List<int> QueryAppsTotalByApplicantType(int clientId, DateTime? startDate, DateTime? endDate)
        {
            // initial query to filter by clientId
            var queryTotals = _dbContext.Applications
                              .SelectMany(application => application.Applicants, (application, applicant) => new { application, applicant })
                              .Where(application => application.applicant.ClientId == clientId);

            // filters by dates
            var filteredQuery = FilterByDates(queryTotals, startDate, endDate, applicationApplicant => applicationApplicant.applicant.CreatedAt);

            // group by applicationId and select the results
            var groupedQuery = filteredQuery
                              .GroupBy(grouping => grouping.application.Id)
                              .Select(grouping => new
                              {
                                  ApplicationId = grouping.Key,
                                  TotalNum = grouping.Count()
                              }).ToList();

            // calculate single and joint applicants totals
            int singleApplicantsTotal = groupedQuery.Count(y => y.TotalNum == 1);
            int jointApplicantsTotal = groupedQuery.Count(y => y.TotalNum == 2);

            // add totals to a list
            List<int> totalsPerAppType = new List<int> { singleApplicantsTotal, jointApplicantsTotal };

            // returns the results as a list
            return totalsPerAppType;
        }

        // create a response of the number of applications per applicant type
        public ApplicationsTotalByApplicantTypeResponse CreateAppsTotalByApplicantTypeResponse(int clientId, DateTime? startDate = null, DateTime? endDate = null)
        {
            // run the query
            var queryOfTotalsPerAppType = QueryAppsTotalByApplicantType(clientId, startDate, endDate);

            // create a response from the query results (num of single and joint applicants)
            return new ApplicationsTotalByApplicantTypeResponse
            {
                SingleApplicants = queryOfTotalsPerAppType[0],
                JointApplicants = queryOfTotalsPerAppType[1]
            };
        }
    }
}