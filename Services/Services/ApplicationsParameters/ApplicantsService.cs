
using Microsoft.AspNetCore.Mvc;
using Services.Enums;
using Services.InputValidation;
using Services.Interfaces.Applicants;

namespace Services.Services.ApplicationsParameters
{
    public class ApplicantsService
    {
        private readonly IApplicationsDataByApplicantTypeService _appTypeAllApps;
        private UserInputValidation _userInputValidation;

        public ApplicantsService(
            IApplicationsDataByApplicantTypeService appTypeAllAppsService,
            UserInputValidation userInputValidationService
        )
        {
            _appTypeAllApps = appTypeAllAppsService;
            _userInputValidation = userInputValidationService;
        }

        public IActionResult GetApplicantResult(int clientId, DateTime? startDate, DateTime? endDate)
        {
            // check if parameters are valid
            _userInputValidation.ValidateIds(clientId);
            // check that start date is before end date
            _userInputValidation.ValidateDates(startDate, endDate);

            var result = _appTypeAllApps.CreateAppsDataByApplicantTypeResponses(clientId, null, startDate, endDate);
            return new OkObjectResult(result);
        }

        public IActionResult GetApplicantByAppIdResult(int clientId, long? applicationId)
        {
            // check if parameters are valid
            _userInputValidation.ValidateIds(clientId, applicationId);

            var result = _appTypeAllApps.CreateAppsDataByApplicantTypeResponses(clientId, applicationId);
            return new OkObjectResult(result);
        }
    }
}
