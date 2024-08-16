using Microsoft.AspNetCore.Mvc;
using Services.Enums;
using Services.InputValidations;
using Services.Responses.Applicants;
using Services.Responses.Applications;
using Services.Services.ApplicationsParameters;

namespace ConsoleApp.Display
{
    public class ApplicationsTotalByApplicantTypeDisplay : BaseDisplay
    {
        public ApplicationsTotalByApplicantTypeDisplay(
            StatusService statusService,
            MetricService metricService,
            ApplicantsService applicantService,
            VariableValidation variableValidator)
            : base(statusService, metricService, applicantService, variableValidator)
        {
        }

        // processes the applicants/count path
        public void ProcessApplicantCount()
        {
            Console.WriteLine("\nPATH: applicants/count");
            int clientId = _variableValidator.ReadInt("Client Id: ");
            MetricTypes metric = _variableValidator.ReadMetric("Metric: ");
            DateTime? startDate = _variableValidator.ReadDateTime("Start Date: ");
            DateTime? endDate = _variableValidator.ReadDateTime("End Date: ");
            var result = _metricService.GetApplicantCountResult(clientId, metric, startDate, endDate);
            GetApplicantCountResponse(result);
        }

        private void GetApplicantCountResponse(IActionResult result)
        {
            Console.WriteLine();
            if (result is ObjectResult objectResult)
            {
                switch (objectResult.Value)
                {
                    case ApplicationsTotalByApplicantTypeResponse response:
                        Console.WriteLine($"NumberOfSingleApplicants: {response.SingleApplicants}");
                        Console.WriteLine($"NumberOfJointApplicants: {response.JointApplicants}");
                        Console.WriteLine();
                        break;
                    case List<ApplicantsByCreditDecisionResponse> responsesCD:
                        foreach (var response in responsesCD)
                        {
                            Console.WriteLine($"ProviderDecisionDescription: {response.ProviderDecisionDescription}");
                            Console.WriteLine($"NumberOfSingleApplicants: {response.SingleApplicants}");
                            Console.WriteLine($"NumberOfJointApplicants: {response.JointApplicants}");
                            Console.WriteLine();
                        }
                        break;
                    case List<ApplicantsByProductTypeResponse> responsesPT:
                        foreach (var response in responsesPT)
                        {
                            Console.WriteLine($"ProviderDecisionDescription: {response.Title}");
                            Console.WriteLine($"NumberOfSingleApplicants: {response.SingleApplicants}");
                            Console.WriteLine($"NumberOfJointApplicants: {response.JointApplicants}");
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