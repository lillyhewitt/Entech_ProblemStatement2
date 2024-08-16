
using Microsoft.AspNetCore.Mvc;
using Services.Enums;
using Services.InputValidation;
using Services.Interfaces.Applications;
using Services.Interfaces.ApplicationsDisbursed;
using Services.Interfaces.ApplicationsIncome;
using Services.Interfaces.CreditScore;

namespace Services.Services.ApplicationsParameters
{
    public class StatusService
    {
        private readonly IApplicationsIncomesService _incomes;
        private readonly IApplicationsCreditScoresService _creditScore;
        private readonly IApplicationsDataService _allLoans;
        private readonly IApplicationsDisbursedDTIService _disbursedLoansDTI;
        private readonly IApplicationsDisbursedAPRService _disbursedLoansAPR;
        private readonly IApplicationsDisbursedInterestRateService _disbursedLoansIR;
        private readonly IApplicationsDisbursedDataService _allDisbursedLoans;
        private UserInputValidation _userInputValidation;

        public StatusService(
            IApplicationsIncomesService incomeService,
            IApplicationsCreditScoresService creditScoreService,
            IApplicationsDataService allLoansService,
            IApplicationsDisbursedDTIService dtiService,
            IApplicationsDisbursedAPRService aprService,
            IApplicationsDisbursedInterestRateService irService,
            IApplicationsDisbursedDataService allDisbursedLoansService,
            UserInputValidation userInputValidationService
        )
        {
            _incomes = incomeService;
            _creditScore = creditScoreService;
            _allLoans = allLoansService;
            _disbursedLoansDTI = dtiService;
            _disbursedLoansAPR = aprService;
            _disbursedLoansIR = irService;
            _creditScore = creditScoreService;
            _allDisbursedLoans = allDisbursedLoansService;
            _userInputValidation = userInputValidationService;
        }

        public IActionResult GetStatusResult(int clientId, StatusTypes status, DateTime? startDate, DateTime? endDate)
        {

            // check if client Id is valid
            _userInputValidation.ValidateIds(clientId);
            // check if status is valid
            _userInputValidation.ValidateStatus(status);
            // check that start date is before end date
            _userInputValidation.ValidateDates(startDate, endDate);

            switch (status)
            {
                case StatusTypes.All:
                    var allLoansResult = _allLoans.CreateAppsDataResponse(clientId, null, startDate, endDate);
                    return new OkObjectResult(allLoansResult);

                case StatusTypes.Disbursed:
                    var disbursedLoansResult = _allDisbursedLoans.CreateDisbursedDataResponses(clientId, null, startDate, endDate);
                    return new OkObjectResult(disbursedLoansResult);

                default:
                    return new BadRequestObjectResult("Invalid type specified. {status}");
            }
        }

        public IActionResult GetStatusByAppIdResult(int clientId, StatusTypes status, long? applicationId)
        {
            // check if client Id and application Id are valid
            _userInputValidation.ValidateIds(clientId, applicationId);
            // check if status is valid
            _userInputValidation.ValidateStatus(status);

            switch (status)
            {
                case StatusTypes.All:
                    var allLoansResult = _allLoans.CreateAppsDataResponse(clientId, applicationId);
                    return new OkObjectResult(allLoansResult);

                case StatusTypes.Disbursed:
                    var disbursedLoansResult = _allDisbursedLoans.CreateDisbursedDataResponses(clientId, applicationId);
                    return new OkObjectResult(disbursedLoansResult);

                default:
                    return new BadRequestObjectResult("Invalid type specified.");
            }
        }

        public IActionResult GetMeanAndMedianResult(
            int clientId,
            StatusTypes status,
            MetricTypes metric,
            decimal? startRange,
            decimal? endRange,
            DateTime? startDate,
            DateTime? endDate)
        {
            // check if client Id valid
            _userInputValidation.ValidateIds(clientId);
            // check if status is valid
            _userInputValidation.ValidateStatus(status);
            // check if ranges are valid
            _userInputValidation.ValidateRanges(startRange, endRange);
            // check that start date is before end date
            _userInputValidation.ValidateDates(startDate, endDate);

            // incomes, dti
            switch ($"{status}_{metric}")
            {
                case "All_CreditScore":
                    var allCreditScoreResult = _creditScore.CreateMeanAndMedianAppsResponse(clientId, startRange, endRange, startDate, endDate);
                    return new OkObjectResult(allCreditScoreResult);
                case "All_Income":
                    var allIncomeResult = _incomes.CreateMeanAndMedianAppsIncomesResponse(clientId, startRange, endRange, startDate, endDate);
                    return new OkObjectResult(allIncomeResult);

                case "Disbursed_Apr":
                    var disbursedAPRResult = _disbursedLoansAPR.CreateMeanAndMedianResponse(clientId, startRange, endRange, startDate, endDate);
                    return new OkObjectResult(disbursedAPRResult);
                case "Disbursed_CreditScore":
                    var disbursedCreditScoreResult = _creditScore.CreateMeanAndMedianAppsDisbursedResponse(clientId, startRange, endRange, startDate, endDate);
                    return new OkObjectResult(disbursedCreditScoreResult);
                case "Disbursed_Dti":
                    var disbursedDTIResult = _disbursedLoansDTI.CreateMeanAndMedianResponse(clientId, startRange, endRange, startDate, endDate);
                    return new OkObjectResult(disbursedDTIResult);
                case "Disbursed_Income":
                    var disbursedIncomeResult = _incomes.CreateMeanAndMedianAppsDisbursedIncomeResponse(clientId, startRange, endRange, startDate, endDate);
                    return new OkObjectResult(disbursedIncomeResult); ;
                case "Disbursed_InterestRate":
                    var disbursedInterestRateResult = _disbursedLoansIR.CreateMeanAndMedianResponse(clientId, startRange, endRange, startDate, endDate);
                    return new OkObjectResult(disbursedInterestRateResult);

                default:
                    return new BadRequestObjectResult("Invalid type specified.");
            }
        }
    }
}
