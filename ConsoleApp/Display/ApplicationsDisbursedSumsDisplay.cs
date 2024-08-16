using Microsoft.AspNetCore.Mvc;
using Services.Enums;
using Services.InputValidations;
using Services.Responses.ApplicationsDisbursed;
using Services.Services.ApplicationsParameters;

namespace ConsoleApp.Display
{
    public class ApplicationsDisbursedSumsDisplay : BaseDisplay
    {
        public ApplicationsDisbursedSumsDisplay(
            StatusService statusService,
            MetricService metricService,
            ApplicantsService applicantService,
            VariableValidation variableValidator)
            : base(statusService, metricService, applicantService, variableValidator)
        {
        }

        // processes the applications/{status}/sum path
        public void ProcessApplicationsSumByStatus()
        {
            Console.WriteLine("\nPATH: applications/{status}/sum");
            int clientId = _variableValidator.ReadInt("Client Id: ");
            StatusTypes status = _variableValidator.ReadStatus("Status: ");
            MetricTypes metric = _variableValidator.ReadMetric("Metric: ");
            DateTime? startDate = _variableValidator.ReadDateTime("Start Date: ");
            DateTime? endDate = _variableValidator.ReadDateTime("End Date: ");
            var result = _metricService.GetDisbursedSumResult(clientId, status, metric, startDate, endDate);
            GetDisbursedSumsResponse(result);
        }

        // processes the applications/{status}/{applicationId}/sum path
        public void ProcessApplicationSumByIdAndStatus()
        {
            Console.WriteLine("\nPATH: applications/{status}/{applicationId}/sum");
            int clientId = _variableValidator.ReadInt("Client Id: ");
            StatusTypes status = _variableValidator.ReadStatus("Status: ");
            long applicationId = _variableValidator.ReadLong("Application Id: ");
            MetricTypes metric = _variableValidator.ReadMetric("Metric: ");
            var result = _metricService.GetDisbursedSumByAppIdResult(clientId, status, metric, applicationId);
            GetDisbursedSumsResponse(result);
        }

        private void GetDisbursedSumsResponse(IActionResult result)
        {
            Console.WriteLine();
            if (result is ObjectResult objectResult)
            {
                switch (objectResult.Value)
                {
                    case ApplicationsDisbursedTotalAmountResponse response:
                        Console.WriteLine($"TotalAmount: {response.TotalAmount}");
                        Console.WriteLine();
                        break;
                    case List<ApplicationsDisbursedMonthlyTotalsResponse> responsesCD:
                        foreach (var response in responsesCD)
                        {
                            Console.WriteLine($"Month: {response.Month}");
                            Console.WriteLine($"Year: {response.Year}");
                            Console.WriteLine($"Amount: {response.Amount}");
                            Console.WriteLine();
                        }
                        break;
                    case ApplicationsDisbursedFinanceChargesResponse responseFC:
                        Console.WriteLine($"Total: {responseFC.Total}");
                        Console.WriteLine();
                        break;

                    default:
                        Console.WriteLine("No applications found. Internal error.");
                        break;
                }
            }
        }
    }
}