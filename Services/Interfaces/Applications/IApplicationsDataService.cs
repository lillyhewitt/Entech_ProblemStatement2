
using Services.QueryResults;
using Services.Responses.Applications;

namespace Services.Interfaces.Applications
{
    public interface IApplicationsDataService
    {
        public List<ApplicationsDataResponse> CreateAppsDataResponse(int clientId, long? applicationId = null, DateTime? startDate = null, DateTime? endDate = null);
    }
}
