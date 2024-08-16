using Microsoft.AspNetCore.Http;
using Services.Interfaces.Applications;
using Services.QueryResults.Applications;
using Services.Responses.Applications;
using Data.Database;

namespace Services.Services.Applications
{
    public sealed class ApplicationsByAllOptionsService : ApplicationsBaseService, IApplicationsByAllOptionsService
    {
        private readonly IApplicationsByApplicationStatusService _appsByAppStatus;
        private readonly IApplicationsByCreditDecisionService _appsByCreditDecision;
        private readonly IApplicationsByProductTypeService _appsByProductType;

        // make database instance 
        public ApplicationsByAllOptionsService(
            AppContext dbContext,
            IHttpContextAccessor httpContextAccessor,
            IApplicationsByApplicationStatusService appStatusService,
            IApplicationsByCreditDecisionService creditDecisionService,
            IApplicationsByProductTypeService productTypeService)
            : base(dbContext, httpContextAccessor)
        {
            _appsByAppStatus = appStatusService;
            _appsByCreditDecision = creditDecisionService;
            _appsByProductType = productTypeService;
        }

        // finds application status, credit decision, and product type for a specific application
        protected ApplicationsByAllOptionsResult QueryAppsByAllOptions(int clientId, long? applicationId, DateTime? startDate, DateTime? endDate)
        {
            // call queries from other services
            var queryAppStatus = _appsByAppStatus.QueryAppsByApplicationStatus(clientId, applicationId, startDate, endDate).FirstOrDefault();
            var queryCreditDecision = _appsByCreditDecision.QueryAppsByCreditDecision(clientId, applicationId, startDate, endDate).FirstOrDefault();
            var queryProductType = _appsByProductType.QueryAppsByProductType(clientId, applicationId, startDate, endDate).FirstOrDefault();

            // combine the queries results into one object
            var combinedQueries = new ApplicationsByAllOptionsResult
            {
                ApplicationStatusResults = queryAppStatus,
                CreditDecisionResults = queryCreditDecision,
                ProductTypeResults = queryProductType
            };

            // return the combined queries object
            return combinedQueries;
        }

        // create list of repsonses for number of applications per application status
        public ApplicationsByAllOptionsResponse CreateAppsByAllOptionsResponse(int clientId, long? applicationId = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            // run the query
            var queryResult = QueryAppsByAllOptions(clientId, applicationId, startDate, endDate);

            // create a list of responses
            var allOptionsResponseList = new ApplicationsByAllOptionsResponse
            {
                ApplicationStatus = queryResult.ApplicationStatusResults?.StatusName,
                CreditDecision = queryResult.CreditDecisionResults?.ProviderDecisionDescription,
                ProductType = queryResult.ProductTypeResults?.DisplayName
            };

            return allOptionsResponseList;
        }
    }
}
