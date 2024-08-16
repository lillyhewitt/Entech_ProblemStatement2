using Microsoft.AspNetCore.Http;
using Services.Interfaces.ApplicationsDisbursed;
using Services.QueryResults.ApplicationsDisbursed;
using Services.Responses.ApplicationsDisbursed;
using Services.Services.Applications;
using Data.Database;

namespace Services.Services.ApplicationsDisbursed
{
    public sealed class ApplicationsDisbursedDataService : ApplicationsBaseService, IApplicationsDisbursedDataService
    {
        // make database instance 
        public ApplicationsDisbursedDataService(
            AppContext dbContext, 
            IHttpContextAccessor httpContextAccessor) 
            : base(dbContext, httpContextAccessor) { }

        // finds all the disbursed loans of an applicationId or for all applications
        protected List<ApplicationsDisbursedDataResult> QueryAppsDisbursed(int clientId, long? applicationId, DateTime? startDate, DateTime? endDate)
        {
            // query to get all the disbursed loans 
            var queryLoanData = _dbContext.ApplicationDisbursements
                            .Where(d => d.ClientId == clientId && d.DisbursedDate != null)
                            .Select(x => new ApplicationsDisbursedDataResult
                            {
                                ApplicationId = x.ApplicationId,
                                DisbursementProfileId = x.DisbursementProfileId,
                                NetAmount = x.NetAmount,
                                GrossAmount = x.GrossAmount,
                                Date = x.ScheduledDate,
                                CreatedAt = x.CreatedAt
                            });

            // filters by applicationId
            var filteredQuery = FilterByApplicationId(queryLoanData, applicationId, item => item.ApplicationId);

            // filters by dates
            filteredQuery = FilterByDates(filteredQuery, startDate, endDate, item => item.CreatedAt);

            return filteredQuery.ToList(); ;
        }

        // format lists of data for disbursed loans
        public List<ApplicationsDisbursedDataResponse> CreateDisbursedDataResponses(int clientId, long? applicationId = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            // run the query
            var queryOfLoanData = QueryAppsDisbursed(clientId, applicationId, startDate, endDate);

            // create a list of the responses of the query results (disbursed loans for specified client)
            var disbursedLoansList = queryOfLoanData
              .Select(item => new ApplicationsDisbursedDataResponse
              {
                  ApplicationId = item.ApplicationId,
                  DisbursementProfileId = item.DisbursementProfileId,
                  NetAmount = item.NetAmount,
                  GrossAmount = item.GrossAmount,
                  Date = item.Date,
                  CreatedAt = item.CreatedAt
              })
              .ToList();

            return disbursedLoansList;
        }
    }
}
