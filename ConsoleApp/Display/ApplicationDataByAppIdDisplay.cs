using Microsoft.AspNetCore.Mvc;
using Services.Enums;
using Services.InputValidations;
using Services.Responses.Applications;
using Services.Responses.ApplicationsDisbursed;
using Services.Services.ApplicationsParameters;

namespace ConsoleApp.Display
{
    public class ApplicationDataByAppIdDisplay : BaseDisplay
    {
        public ApplicationDataByAppIdDisplay(
            StatusService statusService,
            MetricService metricService,
            ApplicantsService applicantService,
            VariableValidation variableValidator)
            : base(statusService, metricService, applicantService, variableValidator)
        {
        }

        // processes the applications/{status}/{applicationId} path
        public void ProcessApplicationByIdAndStatus()
        {
            Console.WriteLine("\nPATH: applications/{status}/{applicationId}");
            int clientId = _variableValidator.ReadInt("Client Id: ");
            StatusTypes status = _variableValidator.ReadStatus("Status: ");
            long applicationId = _variableValidator.ReadLong("Application Id: ");
            var result = _statusService.GetStatusByAppIdResult(clientId, status, applicationId);
            GetApplicationByAppIdResponse(result);
        }

        private void GetApplicationByAppIdResponse(IActionResult result)
        {
            Console.WriteLine();
            if (result is ObjectResult objectResult)
            {
                switch (objectResult.Value)
                {
                    case List<ApplicationsDataResponse> responses:
                        foreach (var response in responses)
                        {
                            Console.WriteLine($"ApplicationId: {response.ApplicationId}");
                            Console.WriteLine($"DateOfBirth: {response.DateOfBirth}");
                            Console.WriteLine($"CreatedAt: {response.CreatedAt}");
                            Console.WriteLine();
                        }
                        break;
                    case List<ApplicationsDisbursedDataResponse> responsesDisbursed:
                        foreach (var response in responsesDisbursed)
                        {
                            Console.WriteLine($"ApplicationId: {response.ApplicationId}");
                            Console.WriteLine($"DisbursementProfileId: {response.DisbursementProfileId}");
                            Console.WriteLine($"NetAmount: {response.NetAmount}");
                            Console.WriteLine($"GrossAmount: {response.GrossAmount}");
                            Console.WriteLine($"Date: {response.Date}");
                            Console.WriteLine($"CreatedAt: {response.CreatedAt}");
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