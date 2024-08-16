using Services.QueryResults.Applications;
using Services.Responses.Applications;

namespace Services.Interfaces.Applications
{
    public interface IApplicationsByCreditDecisionService
    {
        public List<ApplicationsByCreditDecisionsResult> QueryAppsByCreditDecision(int clientId, long? applicationId, DateTime? startDate, DateTime? endDate);
        public List<ApplicationsByCreditDecisionResponse> CreateAppsByCreditDecisionResponses(int clientId, long? applicationId = null, DateTime? startDate = null, DateTime? endDate = null);
        public List<ApplicationByCreditDecisionResponse> CreateAppByCreditDecisionResponses(int clientId, long? applicationId = null, DateTime? startDate = null, DateTime? endDate = null);
    }
}