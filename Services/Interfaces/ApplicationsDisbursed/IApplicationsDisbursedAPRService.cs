
using Services.Responses.ApplicationsDisbursed;

namespace Services.Interfaces.ApplicationsDisbursed
{
    public interface IApplicationsDisbursedAPRService
    {
        public ApplicationsDisbursedAPRResponse CreateMeanAndMedianResponse(int clientId, decimal? startRange = null, decimal? endRange = null, DateTime? startDate = null, DateTime? endDate = null);
    }
}
