
using Services.QueryResults;
using Services.Responses.Applicants;

namespace Services.Interfaces.Applicants
{
    public interface IApplicantsByProductTypeService
    {
        public List<ApplicantsByProductTypeResponse> CreateAppsByProductTypeResponses(int clientId, DateTime? startDate = null, DateTime? endDate = null);
        public List<ApplicantByProductTypeResponse> CreateAppByProductTypeResponses(int clientId, long? applicationId = null);
    }
}
