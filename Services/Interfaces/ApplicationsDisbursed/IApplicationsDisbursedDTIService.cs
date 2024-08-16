
using Services.Responses.ApplicationsDisbursed;

namespace Services.Interfaces.ApplicationsDisbursed
{
    public interface IApplicationsDisbursedDTIService
    {
        public ApplicationsDisbursedDTIResponse CreateMeanAndMedianResponse(int clientId, decimal? startRange = null, decimal? endRange = null, DateTime? startDate = null, DateTime? endDate = null);
    }
}
