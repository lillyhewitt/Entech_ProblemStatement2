using Services.QueryResults;
using Services.Responses.ApplicationsDisbursed;

namespace Services.Interfaces.ApplicationsDisbursed
{
    public interface IApplicationsDisbursedDataService
    {
        public List<ApplicationsDisbursedDataResponse> CreateDisbursedDataResponses(int clientId, long? applicationId = null, DateTime? startDate = null, DateTime? endDate = null);
    }
}