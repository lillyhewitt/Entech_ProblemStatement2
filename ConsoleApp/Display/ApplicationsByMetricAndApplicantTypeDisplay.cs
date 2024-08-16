using Microsoft.AspNetCore.Mvc;
using Services.Enums;
using Services.InputValidations;
using Services.Responses.Applications;
using Services.Services.ApplicationsParameters;

namespace ConsoleApp.Display
{
    public class ApplicationsByMetricAndApplicantTypeDisplay : BaseDisplay
    {
        public ApplicationsByMetricAndApplicantTypeDisplay(
            StatusService statusService,
            MetricService metricService,
            ApplicantsService applicantService,
            VariableValidation variableValidator)
            : base(statusService, metricService, applicantService, variableValidator)
        {
        }

        // processes the applications/{status}/{applicationId}/find path
        public void ProcessApplicationFindByStatus()
        {
            Console.WriteLine("\nPATH: applications/{status}/{applicationId}/find");
            int clientId = _variableValidator.ReadInt("Client Id: ");
            StatusTypes status = _variableValidator.ReadStatus("Status: ");
            long applicationId = _variableValidator.ReadLong("Application Id: ");
            MetricTypes metric = _variableValidator.ReadMetric("Metric: ");
            var result = _metricService.GetStatusFindResult(clientId, status, metric, applicationId);
            GetApplicationsFindByAppIdResponse(result);
        }
        private void GetApplicationsFindByAppIdResponse(IActionResult result)
        {
            Console.WriteLine();
            if (result is ObjectResult objectResult)
            {
                switch (objectResult.Value)
                {
                    case ApplicationsByAllOptionsResponse response:
                        Console.WriteLine($"ApplicationStatus: {response.ApplicationStatus}");
                        Console.WriteLine($"CreditDecision: {response.CreditDecision}");
                        Console.WriteLine($"ProductType: {response.ProductType}");
                        Console.WriteLine();
                        break;
                    case List<ApplicationByCreditDecisionResponse> responsesCD:
                        foreach (var response in responsesCD)
                        {
                            Console.WriteLine($"CreditDecision: {response.CreditDecision}");
                            Console.WriteLine();
                        }
                        break;
                    case List<ApplicationByProductTypeResponse> responsesPT:
                        foreach (var response in responsesPT)
                        {
                            Console.WriteLine($"ProductType: {response.ProductType}");
                            Console.WriteLine();
                        }
                        break;
                    case List<ApplicationByApplicationStatusResponse> responsesAS:
                        foreach (var response in responsesAS)
                        {
                            Console.WriteLine($"ApplicationStatus: {response.ApplicationStatus}");
                            Console.WriteLine();
                        }
                        break;

                    default:
                        Console.WriteLine("No applications found. Internal error.");
                        break;
                }
            }
        }
    }
}