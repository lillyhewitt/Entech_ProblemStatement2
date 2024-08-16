using Services.QueryResults.Applications;
using Services.Responses.Applications;

namespace Services.Interfaces.Applications
{
    public interface IApplicationsByProductTypeService
    {
        public List<ApplicationsByProductTypeResult> QueryAppsByProductType(int clientId, long? applicationId, DateTime? startDate, DateTime? endDate);
        public List<ApplicationsByProductTypeResponse> CreateAppsByProductTypeResponses(int clientId, long? applicationId = null, DateTime? startDate = null, DateTime? endDate = null);
        public List<ApplicationByProductTypeResponse> CreateAppByProductTypeResponses(int clientId, long? applicationId = null, DateTime? startDate = null, DateTime? endDate = null);
    }
}