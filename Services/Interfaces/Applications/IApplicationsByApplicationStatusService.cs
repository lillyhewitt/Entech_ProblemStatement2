
using Services.QueryResults.Applications;
using Services.Responses.Applications;

namespace Services.Interfaces.Applications
{
    public interface IApplicationsByApplicationStatusService
    {
        public List<ApplicationsByApplicationStatusResult> QueryAppsByApplicationStatus(int clientId, long? applicationId, DateTime? startDate, DateTime? endDate);
        public List<ApplicationsByApplicationStatusResponse> CreateAppsByApplicationStatusResponses(int clientId, long? applicationId = null, DateTime? startDate = null, DateTime? endDate = null);
        public List<ApplicationByApplicationStatusResponse> CreateAppByApplicationStatusResponses(int clientId, long? applicationId = null, DateTime? startDate = null, DateTime? endDate = null);
    }
}
