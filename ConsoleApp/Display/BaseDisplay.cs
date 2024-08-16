using Services.InputValidations;
using Services.Services.ApplicationsParameters;

namespace ConsoleApp.Display
{
    public abstract class BaseDisplay
    {
        protected readonly StatusService _statusService;
        protected readonly MetricService _metricService;
        protected readonly ApplicantsService _applicantService;
        protected readonly VariableValidation _variableValidator;

        public BaseDisplay(
            StatusService statusService,
            MetricService metricService,
            ApplicantsService applicantService,
            VariableValidation variableValidator)
        {
            _statusService = statusService;
            _metricService = metricService;
            _applicantService = applicantService;
            _variableValidator = variableValidator;
        }
    }
}