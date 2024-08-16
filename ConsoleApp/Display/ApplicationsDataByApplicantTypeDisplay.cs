using Microsoft.AspNetCore.Mvc;
using Services.InputValidations;
using Services.Responses.Applicants;
using Services.Services.ApplicationsParameters;

namespace ConsoleApp.Display
{
    public class ApplicationsDataByApplicantTypeDisplay : BaseDisplay
    {
        public ApplicationsDataByApplicantTypeDisplay(
            StatusService statusService,
            MetricService metricService,
            ApplicantsService applicantService,
            VariableValidation variableValidator)
            : base(statusService, metricService, applicantService, variableValidator)
        {
        }

        // processes the applicants path
        public void ProcessApplicants()
        {
            Console.WriteLine("\nPATH: applicants");
            int clientId = _variableValidator.ReadInt("Client Id: ");
            DateTime? startDate = _variableValidator.ReadDateTime("Start Date: ");
            DateTime? endDate = _variableValidator.ReadDateTime("End Date: ");
            var result = _applicantService.GetApplicantResult(clientId, startDate, endDate);
            GetApplicantDataResponse(result);
        }

        // processes the applicants/{applicationId} path
        public void ProcessApplicantById()
        {
            Console.WriteLine("\nPATH: applicants/{applicationId}");
            int clientId = _variableValidator.ReadInt("Client Id: ");
            long applicationId = _variableValidator.ReadLong("Application Id: ");
            var result = _applicantService.GetApplicantByAppIdResult(clientId, applicationId);
            GetApplicantDataResponse(result);
        }

        private void GetApplicantDataResponse(IActionResult result)
        {
            Console.WriteLine();
            if (result is ObjectResult objectResult)
            {
                switch (objectResult.Value)
                {
                    case List<ApplicationsDataByApplicantTypeResponse> responses:
                        foreach (var response in responses)
                        {
                            Console.WriteLine($"ApplicationId: {response.ApplicationId}");
                            Console.WriteLine($"IsJointApplicant: {response.IsJointApplicant}");
                            Console.WriteLine($"CreatedAt: {response.CreatedAt}");
                            Console.WriteLine($"WithdrawDate: {response.WithdrawDate}");
                            Console.WriteLine($"CreditExpirationDate: {response.CreditExpirationDate}");
                            Console.WriteLine($"OfferExpirationDate: {response.OfferExpirationDate}");
                            Console.WriteLine($"FinalApprovalDate: {response.FinalApprovalDate}");
                            Console.WriteLine($"RightToCancelDate: {response.RightToCancelDate}");
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