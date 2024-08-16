using Services.Interfaces.ApplicationsDisbursed;
using Services.Responses.ApplicationsDisbursed;
using Services.Services.Applications;
using Microsoft.AspNetCore.Http;
using Data.Database;

namespace Services.Services.ApplicationsDisbursed
{
    public sealed class ApplicationsDisbursedTotalAmountService : ApplicationsBaseService, IApplicationsDisbursedTotalAmountService
    {
        // make database instance 
        public ApplicationsDisbursedTotalAmountService(AppContext dbContext, IHttpContextAccessor httpContextAccessor) : base(dbContext, httpContextAccessor) { }

        // sums the total funds disbursed from a specific applicationId
        protected decimal QueryAppsDisbursedTotalAmount(int clientId, long? applicationId, DateTime? startDate, DateTime? endDate)
        {
            // query to get the total funds disbursed all time
            var queryTotalFunds = _dbContext.ApplicationDisbursements
                                .Where(applicationDisbursement => applicationDisbursement.ClientId == clientId && applicationDisbursement.DisbursedDate != null)
                                .Select(applicationDisbursement => new
                                {
                                    applicationDisbursement.ApplicationId,
                                    applicationDisbursement.NetAmount,
                                    applicationDisbursement.CreatedAt
                                });

            // filters by applicationId
            var filteredQuery = FilterByApplicationId(queryTotalFunds, applicationId, item => item.ApplicationId);

            // filters by dates
            filteredQuery = FilterByDates(filteredQuery, startDate, endDate, item => item.CreatedAt);

            // sum the total funds
            var totalFunds = filteredQuery.ToList().Sum(x => x.NetAmount ?? 0.0m);

            // return the query results (funds for specified client or all applications)
            return totalFunds;
        }

        // create response for the total funds from disbursed loans
        public ApplicationsDisbursedTotalAmountResponse CreateAppsDisbursedTotalAmountResponse(int clientId, long? applicationId = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            // run the query
            var queryOfTotalNum = QueryAppsDisbursedTotalAmount(clientId, applicationId, startDate, endDate);

            // create a response of the query results (funds for specified client)
            return new ApplicationsDisbursedTotalAmountResponse
            {
                TotalAmount = queryOfTotalNum
            };
        }
    }
}
