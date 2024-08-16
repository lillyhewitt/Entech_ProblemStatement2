using Microsoft.AspNetCore.Mvc;
using Services.Enums;
using Services.InputValidations;
using Services.Responses.Applications;
using Services.Responses.ApplicationsDisbursed;
using Services.Responses.ApplicationsIncome;
using Services.Responses.CreditScore;
using Services.Services.ApplicationsParameters;

namespace ConsoleApp.Display
{
    public class MeanAndMedianDisplay : BaseDisplay
    {
        public MeanAndMedianDisplay(
            StatusService statusService,
            MetricService metricService,
            ApplicantsService applicantService,
            VariableValidation variableValidator)
            : base(statusService, metricService, applicantService, variableValidator)
        {
        }

        // processes the applications/{status}/mean-and-median path
        public void ProcessApplicationsMeanAndMedian()
        {
            Console.WriteLine("\nPATH: applications/{status}/mean-and-median");
            int clientId = _variableValidator.ReadInt("Client Id: ");
            StatusTypes status = _variableValidator.ReadStatus("Status: ");
            MetricTypes metric = _variableValidator.ReadMetric("Metric: ");
            decimal? startRange = _variableValidator.ReadDecimal("Start Range: ");
            decimal? endRange = _variableValidator.ReadDecimal("End Range: ");
            DateTime? startDate = _variableValidator.ReadDateTime("Start Date: ");
            DateTime? endDate = _variableValidator.ReadDateTime("End Date: ");
            var result = _statusService.GetMeanAndMedianResult(clientId, status, metric, startRange, endRange, startDate, endDate);
            GetMeanAndMedianResponse(result);
        }

        private void GetMeanAndMedianResponse(IActionResult result)
        {
            Console.WriteLine();
            if (result is ObjectResult objectResult)
            {
                switch (objectResult.Value)
                {
                    case ApplicationsCreditScoresResponse responseCD:
                        Console.WriteLine($"Average: {responseCD.Average}");
                        Console.WriteLine($"Median: {responseCD.Median}");
                        Console.WriteLine();
                        break;
                    case ApplicationsAveragesResponse responseDisCD:
                        Console.WriteLine($"Average: {responseDisCD.Average}");
                        Console.WriteLine($"Median: {responseDisCD.Median}");
                        Console.WriteLine();
                        break;
                    case ApplicationsIncomesResponse responseI:
                        Console.WriteLine($"Average: {responseI.Average}");
                        Console.WriteLine($"Median: {responseI.Median}");
                        Console.WriteLine();
                        break;
                    case ApplicationsDisbursedIncomeResponse responseDisI:
                        Console.WriteLine($"Average: {responseDisI.Average}");
                        Console.WriteLine($"Median: {responseDisI.Median}");
                        Console.WriteLine();
                        break;
                    case ApplicationsDisbursedAPRResponse responseDisAPR:
                        Console.WriteLine($"Average: {responseDisAPR.Average}");
                        Console.WriteLine($"Median: {responseDisAPR.Median}");
                        Console.WriteLine();
                        break;
                    case ApplicationsDisbursedDTIResponse responseDisDTI:
                        Console.WriteLine($"Average: {responseDisDTI.Average}");
                        Console.WriteLine($"Median: {responseDisDTI.Median}");
                        Console.WriteLine();
                        break;
                    case ApplicationsDisbursedInterestRateResponse responseDisIR:
                        Console.WriteLine($"Average: {responseDisIR.Average}");
                        Console.WriteLine($"Median: {responseDisIR.Median}");
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