
using Services.QueryResults;
using Services.Responses.Applicants;

namespace Services.Interfaces.Applicants
{
    public interface IApplicationsDataByApplicantTypeService
    {
        public List<ApplicationsDataByApplicantTypeResponse> CreateAppsDataByApplicantTypeResponses(int clientId, long? applicationId = null, DateTime? startDate = null, DateTime? endDate = null);
    }
}
