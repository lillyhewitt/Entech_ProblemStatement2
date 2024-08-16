
using Services.Responses.Applications;

namespace Services.Interfaces.ApplicationsDisbursed
{
    public interface IApplicationsTotalService
    {
        public ApplicationsTotalResponse CreateAppsTotalResponse(int clientId, DateTime? startDate = null, DateTime? endDate = null);
    }
}
