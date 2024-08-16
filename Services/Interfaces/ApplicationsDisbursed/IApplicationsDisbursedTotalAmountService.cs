
using Services.Responses.ApplicationsDisbursed;

namespace Services.Interfaces.ApplicationsDisbursed
{
    public interface IApplicationsDisbursedTotalAmountService
    {
        public ApplicationsDisbursedTotalAmountResponse CreateAppsDisbursedTotalAmountResponse(int clientId, long? applicationId = null, DateTime? startDate = null, DateTime? endDate = null);
    }
}
