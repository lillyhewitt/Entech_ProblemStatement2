using Services.QueryResults;
using Services.Responses.Applicants;

namespace Services.Interfaces.Applicants
{
    public interface IApplicantsByCreditDecisionService
    {
        public List<ApplicantsByCreditDecisionResponse> CreateAppsByCreditDecisionResponses(int clientId, DateTime? startDate = null, DateTime? endDate = null);
        public List<ApplicantByCreditDecisionResponse> CreateAppByCreditDecisionResponses(int clientId, long? applicationId = null);
    }
}
