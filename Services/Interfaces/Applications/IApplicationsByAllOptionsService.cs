using Services.QueryResults;
using Services.Responses.Applications;

namespace Services.Interfaces.Applications
{
    public interface IApplicationsByAllOptionsService
    {
        public ApplicationsByAllOptionsResponse CreateAppsByAllOptionsResponse(int clientId, long? applicationId = null, DateTime? startDate = null, DateTime? endDate = null);
    }
}
