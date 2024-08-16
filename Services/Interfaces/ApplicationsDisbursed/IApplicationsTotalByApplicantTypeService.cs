
using Services.Responses.Applications;

namespace Services.Interfaces.ApplicationsDisbursed
{
    public interface IApplicationsTotalByApplicantTypeService
    {
        public ApplicationsTotalByApplicantTypeResponse CreateAppsTotalByApplicantTypeResponse(int clientId, DateTime? startDate, DateTime? endDate);
    }
}
