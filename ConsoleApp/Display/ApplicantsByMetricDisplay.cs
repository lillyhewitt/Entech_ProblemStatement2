using Microsoft.AspNetCore.Mvc;
using Services.Enums;
using Services.InputValidations;
using Services.Responses.Applicants;
using Services.Services.ApplicationsParameters;

namespace ConsoleApp.Display
{
    public class ApplicantsByMetricDisplay : BaseDisplay
    {
        public ApplicantsByMetricDisplay(
            StatusService statusService,
            MetricService metricService,
            ApplicantsService applicantService,
            VariableValidation variableValidator)
            : base(statusService, metricService, applicantService, variableValidator)
        {
        }

        // processes the applicants/{applicationId}/find path
        public void ProcessApplicantFind()
        {
            Console.WriteLine("\nPATH: applicants/{applicationId}/find");
            int clientId = _variableValidator.ReadInt("Client Id: ");
            long applicationId = _variableValidator.ReadLong("Application Id: ");
            MetricTypes metric = _variableValidator.ReadMetric("Metric: ");
            var result = _metricService.GetApplicantFindResult(clientId, metric, applicationId);
            GetApplicantFindResponse(result);
        }

        private void GetApplicantFindResponse(IActionResult result)
        {
            Console.WriteLine();
            if (result is ObjectResult objectResult)
            {
                switch (objectResult.Value)
                {
                    case List<ApplicantByCreditDecisionResponse> responses:
                        foreach (var response in responses)
                        {
                            Console.WriteLine($"ProviderDecisionDescription: {response.ProviderDecisionDescription}");
                            Console.WriteLine($"IsJointApplicant: {response.IsJointApplicant}");
                            Console.WriteLine();
                        }
                        break;
                    case List<ApplicantByProductTypeResponse> productResponses:
                        foreach (var response in productResponses)
                        {
                            Console.WriteLine($"ProductType: {response.DisplayName}");
                            Console.WriteLine($"IsJointApplicant: {response.IsJointApplicant}");
                            Console.WriteLine();
                        }
                        break;

                    default:
                        Console.WriteLine("No applicants found. Internal error.");
                        break;
                }
            }
        }
    }
}