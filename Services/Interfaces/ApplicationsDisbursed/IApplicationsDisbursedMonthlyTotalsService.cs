
using Services.QueryResults;
using Services.Responses.ApplicationsDisbursed;

namespace Services.Interfaces.ApplicationsDisbursed
{
    public interface IApplicationsDisbursedMonthlyTotalsService
    {
        public List<ApplicationsDisbursedMonthlyTotalsResponse> CreateAppsDisbursedMonthlyTotalsResponses(int clientId, long? applicationId = null, DateTime? startDate = null, DateTime? endDate = null);
    }
}
