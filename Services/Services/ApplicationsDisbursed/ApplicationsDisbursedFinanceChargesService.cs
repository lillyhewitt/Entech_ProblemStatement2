using Data.Database;
using Data.Validations;
using Microsoft.AspNetCore.Http;
using Services.Interfaces.ApplicationsDisbursed;
using Services.Responses.ApplicationsDisbursed;

namespace Services.Services.ApplicationsDisbursed
{
    public sealed class ApplicationsDisbursedFinanceChargesService : ApplicationsDisbursedBaseService, IApplicationsDisbursedFinanceChargesService
    {
        // make database instance 
        public ApplicationsDisbursedFinanceChargesService(
            AppContext dbContext,
            IHttpContextAccessor httpContextAccessor,
            DisbursementStateIdsValidation disbursementStateIds)
            : base(dbContext, httpContextAccessor, disbursementStateIds) { }

        // finds total finance charges across all disbursed loans
        protected double QueryAppsDisbursedFinanceCharges(int clientId, long? applicationId, DateTime? startDate, DateTime? endDate)
        {
            // find the total finance charges for disbursed loans
            var queryTotal = from ac in _dbContext.ApplicationConsolidations
                             join dsm in _dbContext.DisbursementStateManagements
                                on ac.ApplicationDisbursementId equals dsm.Id
                             join ap in _dbContext.ApplicationPrices
                                on ac.ApplicationId equals ap.ApplicationId
                             join ad in _dbContext.ApplicationDisbursements
                                on ap.ApplicationId equals ad.ApplicationId
                             where RetrieveDisbursementStateIds().Contains(dsm.DisbursementStateId)
                                   && ac.ClientId == clientId
                                   && ad.DisbursedDate != null
                             select new
                             {
                                 ad.NetAmount,
                                 ad.CreatedAt,
                                 ap.ApplicationId
                             };

            // filters by applicationId
            var filteredQuery = FilterByApplicationId(queryTotal, applicationId, item => item.ApplicationId);

            // filters by dates
            filteredQuery = FilterByDates(filteredQuery, startDate, endDate, item => item.CreatedAt);

            // sum the total finance charges
            var total = (double)filteredQuery.ToList().Sum(x => x.NetAmount ?? 0);

            // return the results of the query (total finance charges)
            return total;
        }

        // creates a response for the total finance charges
        public ApplicationsDisbursedFinanceChargesResponse CreateAppsDisbursedFinanceChargesResponse(int clientId, long? applicationId = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            // run the query
            var queryResult = QueryAppsDisbursedFinanceCharges(clientId, applicationId, startDate, endDate);

            // create a response of the result (total finance charges) 
            return new ApplicationsDisbursedFinanceChargesResponse
            {
                Total = Math.Round(queryResult, 2)
            };
        }
    }
}