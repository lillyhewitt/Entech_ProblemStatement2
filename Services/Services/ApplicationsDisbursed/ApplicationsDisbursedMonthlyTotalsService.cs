using Microsoft.AspNetCore.Http;
using Services.Interfaces.ApplicationsDisbursed;
using Services.QueryResults.ApplicationsDisbursed;
using Services.Responses.ApplicationsDisbursed;
using Services.Services.Applications;
using Data.Database;

namespace Services.Services.ApplicationsDisbursed
{
    public sealed class ApplicationsDisbursedMonthlyTotalsService : ApplicationsBaseService, IApplicationsDisbursedMonthlyTotalsService
    {
        // make database instance 
        public ApplicationsDisbursedMonthlyTotalsService(AppContext dbContext, IHttpContextAccessor httpContextAccessor) : base(dbContext, httpContextAccessor) { }

        // counts the total funds disbursed all time by month of a specific applicant or all applications
        protected List<ApplicationsDisbursedMonthlyTotalsResult> QueryAppsDisbursedMonthlyTotals(int clientId, long? applicationId, DateTime? startDate, DateTime? endDate)
        {
            // initial query to filter by clientId and non-null DisbursedDate
            var query = _dbContext.ApplicationDisbursements
                .Where(applicationDisbursement => applicationDisbursement.ClientId == clientId && applicationDisbursement.DisbursedDate != null);

            // filters by applicationId
            var filteredQuery = FilterByApplicationId(query, applicationId, item => item.ApplicationId);

            // filters by dates
            filteredQuery = FilterByDates(filteredQuery, startDate, endDate, item => item.CreatedAt);

            // group by applicationId, year, and month and select data on those items
            var querySum = filteredQuery
                .GroupBy(applicationDisbursement => new
                {
                    applicationDisbursement.DisbursedDate.Value.Year,
                    applicationDisbursement.DisbursedDate.Value.Month
                })
                .Select(grouping => new ApplicationsDisbursedMonthlyTotalsResult
                {
                    ApplicationId = grouping.FirstOrDefault().ApplicationId,
                    Year = grouping.Key.Year,
                    Month = grouping.Key.Month,
                    Amount = grouping.Sum(d => d.NetAmount)
                });

            // return the query results (sum of disbursed loans by month)
            return querySum.ToList();
        }

        // create list of responses to show the total monthly funds from disbursed loans
        public List<ApplicationsDisbursedMonthlyTotalsResponse> CreateAppsDisbursedMonthlyTotalsResponses(int clientId, long? applicationId = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            // run the query
            var queryOfMonthlyTotals = QueryAppsDisbursedMonthlyTotals(clientId, applicationId, startDate, endDate);

            // create a list of responses from the query results (sum of disbursed loans by month)
            var monthlyDisbursedLoansList = queryOfMonthlyTotals
              .Select(item => new ApplicationsDisbursedMonthlyTotalsResponse
              {
                  Month = item.Month,
                  Year = item.Year,
                  Amount = item.Amount
              })
              .ToList();

            return monthlyDisbursedLoansList;
        }
    }
}
