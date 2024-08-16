using Microsoft.AspNetCore.Mvc;
using Services.Enums;
using Services.InputValidations;
using Services.Responses.Applications;
using Services.Services.ApplicationsParameters;

namespace ConsoleApp.Display
{
    public class ApplicationsByMetricDisplay : BaseDisplay
    {
        public ApplicationsByMetricDisplay(
            StatusService statusService,
            MetricService metricService,
            ApplicantsService applicantService,
            VariableValidation variableValidator)
            : base(statusService, metricService, applicantService, variableValidator)
        {
        }

        // processes the applications/{status}/count path
        public void ProcessApplicationsCountByStatus()
        {
            Console.WriteLine("\nPATH: applications/{status}/count");
            int clientId = _variableValidator.ReadInt("Client Id: ");
            StatusTypes status = _variableValidator.ReadStatus("Status: ");
            MetricTypes metric = _variableValidator.ReadMetric("Metric: ");
            DateTime? startDate = _variableValidator.ReadDateTime("Start Date: ");
            DateTime? endDate = _variableValidator.ReadDateTime("End Date: ");
            var result = _metricService.GetStatusCountResult(clientId, status, metric, startDate, endDate);
            GetApplicationsCountResponse(result);
        }

        private void GetApplicationsCountResponse(IActionResult result)
        {
            Console.WriteLine();
            if (result is ObjectResult objectResult)
            {
                switch (objectResult.Value)
                {
                    case ApplicationsTotalResponse response:
                        Console.WriteLine($"Loans: {response.Loans}");
                        Console.WriteLine();
                        break;
                    case List<ApplicationsByCreditDecisionResponse> responsesCD:
                        foreach (var response in responsesCD)
                        {
                            Console.WriteLine($"CreditDecision: {response.CreditDecision}");
                            Console.WriteLine($"Number: {response.Number}");
                            Console.WriteLine();
                        }
                        break;
                    case List<ApplicationsByProductTypeResponse> responsesPT:
                        foreach (var response in responsesPT)
                        {
                            Console.WriteLine($"ProductType: {response.ProductType}");
                            Console.WriteLine($"Number: {response.Number}");
                            Console.WriteLine();
                        }
                        break;
                    case List<ApplicationsByApplicationStatusResponse> responsesAS:
                        foreach (var response in responsesAS)
                        {
                            Console.WriteLine($"ApplicationStatus: {response.ApplicationStatus}");
                            Console.WriteLine($"Number: {response.Number}");
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