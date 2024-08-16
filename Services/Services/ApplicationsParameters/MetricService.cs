
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using MySqlX.XDevAPI;
using Services.Enums;
using Services.InputValidation;
using Services.Interfaces.Applicants;
using Services.Interfaces.Applications;
using Services.Interfaces.ApplicationsDisbursed;

namespace Services.Services.ApplicationsParameters
{
    public class MetricService
    {
        private readonly IApplicationsTotalByApplicantTypeService _appType;
        private readonly IApplicantsByCreditDecisionService _appTypeCD;
        private readonly IApplicantsByProductTypeService _appTypePT;
        private readonly IApplicationsDataByApplicantTypeService _appTypeAllApps;
        private readonly IApplicationsTotalService _totalLoans;
        private readonly IApplicationsByCreditDecisionService _byCreditDecisions;
        private readonly IApplicationsByApplicationStatusService _byAppStatuses;
        private readonly IApplicationsByProductTypeService _byProductTypes;
        private readonly IApplicationsByAllOptionsService _byAllOptions;
        private readonly IApplicationsDisbursedTotalAmountService _disbursedLoansTotal;
        private readonly IApplicationsDisbursedMonthlyTotalsService _disbursedLoansMT;
        private readonly IApplicationsDisbursedFinanceChargesService _disbursedLoansFC;
        private UserInputValidation _userInputValidation;

        public MetricService(
            IApplicationsTotalByApplicantTypeService appType,
            IApplicantsByCreditDecisionService appTypeCD,
            IApplicantsByProductTypeService appTypePT,
            IApplicationsDataByApplicantTypeService appTypeAllAppsService,
            IApplicationsTotalService totalLoanService,
            IApplicationsByCreditDecisionService creditDecisionsService,
            IApplicationsByApplicationStatusService appStatusService,
            IApplicationsByProductTypeService productTypesService,
            IApplicationsByAllOptionsService allOptionsService,
            IApplicationsDisbursedTotalAmountService disbursedLoanService,
            IApplicationsDisbursedMonthlyTotalsService monthlyTotalsService,
            IApplicationsDisbursedFinanceChargesService chargesService,
            UserInputValidation userInputValidationService)
        {
            _appType = appType;
            _appTypeCD = appTypeCD;
            _appTypePT = appTypePT;
            _appTypeAllApps = appTypeAllAppsService;
            _totalLoans = totalLoanService;
            _byCreditDecisions = creditDecisionsService;
            _byAppStatuses = appStatusService;
            _byProductTypes = productTypesService;
            _byAllOptions = allOptionsService;
            _disbursedLoansTotal = disbursedLoanService;
            _disbursedLoansMT = monthlyTotalsService;
            _disbursedLoansFC = chargesService;
            _userInputValidation = userInputValidationService;
        }

        public IActionResult GetApplicantCountResult(int clientId, MetricTypes? metric, DateTime? startDate, DateTime? endDate)
        {

            // check if client Id are valid
            _userInputValidation.ValidateIds(clientId);
            // check that start date is before end date
            _userInputValidation.ValidateDates(startDate, endDate);

            switch (metric)
            {
                case null:
                    var totalResult = _appType.CreateAppsTotalByApplicantTypeResponse(clientId, startDate, endDate);
                    return new OkObjectResult(totalResult);
                case MetricTypes.All:
                    var totalResult2 = _appType.CreateAppsTotalByApplicantTypeResponse(clientId, startDate, endDate);
                    return new OkObjectResult(totalResult2);
                case MetricTypes.CreditDecisions:
                    var creditDecisionsResult = _appTypeCD.CreateAppsByCreditDecisionResponses(clientId, startDate, endDate);
                    return new OkObjectResult(creditDecisionsResult);
                case MetricTypes.ProductTypes:
                    var productTypesResult = _appTypePT.CreateAppsByProductTypeResponses(clientId, startDate, endDate);
                    return new OkObjectResult(productTypesResult);
                default:
                    return new BadRequestObjectResult("Invalid metric specified.");
            }
        }

        public IActionResult GetApplicantFindResult(int clientId, MetricTypes? metric, long? applicationId = null)
        {
            // check if client Id are valid
            _userInputValidation.ValidateIds(clientId, applicationId);

            switch (metric)
            {
                case MetricTypes.CreditDecisions:
                    var creditDecisionsResult = _appTypeCD.CreateAppByCreditDecisionResponses(clientId, applicationId);
                    return new OkObjectResult(creditDecisionsResult);
                case MetricTypes.ProductTypes:
                    var productTypesResult = _appTypePT.CreateAppByProductTypeResponses(clientId, applicationId);
                    return new OkObjectResult(productTypesResult);

                default:
                    return new BadRequestObjectResult("Invalid metric specified.");
            }
        }

        public IActionResult GetStatusCountResult(
            int clientId,
            StatusTypes status,
            MetricTypes? metric,
            DateTime? startDate,
            DateTime? endDate)
        {
            // check if client Id is valid
            _userInputValidation.ValidateIds(clientId);
            // check if status is valid
            _userInputValidation.ValidateAllStatus(status);
            // check that start date is before end date
            _userInputValidation.ValidateDates(startDate, endDate);

            switch (metric)
            {
                case null:
                    var totalResult = _totalLoans.CreateAppsTotalResponse(clientId, startDate, endDate);
                    return new OkObjectResult(totalResult);
                case MetricTypes.All:
                    var allResults = _totalLoans.CreateAppsTotalResponse(clientId, startDate, endDate);
                    return new OkObjectResult(allResults);
                case MetricTypes.CreditDecisions:
                    var creditDecisionsResult = _byCreditDecisions.CreateAppsByCreditDecisionResponses(clientId, null, startDate, endDate);
                    return new OkObjectResult(creditDecisionsResult);
                case MetricTypes.ApplicationStatuses:
                    var applicationStatusesResult = _byAppStatuses.CreateAppsByApplicationStatusResponses(clientId, null, startDate, endDate);
                    return new OkObjectResult(applicationStatusesResult);
                case MetricTypes.ProductTypes:
                    var productTypesResult = _byProductTypes.CreateAppsByProductTypeResponses(clientId, null, startDate, endDate);
                    return new OkObjectResult(productTypesResult);

                default:
                    return new BadRequestObjectResult("Invalid metric specified.");
            }
        }

        public IActionResult GetStatusFindResult(
            int clientId,
            StatusTypes status,
            MetricTypes? metric,
            long? applicationId = null)
        {
            // check if client Id and application Id are valid
            _userInputValidation.ValidateIds(clientId, applicationId);
            // check if status is valid 
            _userInputValidation.ValidateAllStatus(status);

            switch (metric)
            {
                case null:
                    var allResults = _byAllOptions.CreateAppsByAllOptionsResponse(clientId, applicationId);
                    return new OkObjectResult(allResults);
                case MetricTypes.All:
                    var allResults2 = _byAllOptions.CreateAppsByAllOptionsResponse(clientId, applicationId);
                    return new OkObjectResult(allResults2);
                case MetricTypes.CreditDecisions:
                    var creditDecisionsResult = _byCreditDecisions.CreateAppByCreditDecisionResponses(clientId, applicationId);
                    return new OkObjectResult(creditDecisionsResult);
                case MetricTypes.ApplicationStatuses:
                    var applicationStatusesResult = _byAppStatuses.CreateAppByApplicationStatusResponses(clientId, applicationId);
                    return new OkObjectResult(applicationStatusesResult);
                case MetricTypes.ProductTypes:
                    var productTypesResult = _byProductTypes.CreateAppByProductTypeResponses(clientId, applicationId);
                    return new OkObjectResult(productTypesResult);

                default:
                    return new BadRequestObjectResult("Invalid metric specified.");
            }
        }

        public IActionResult GetDisbursedSumResult(
            int clientId,
            StatusTypes status,
            MetricTypes? metric,
            DateTime? startDate = null,
            DateTime? endDate = null)
        {
            // check if client Id is valid
            _userInputValidation.ValidateIds(clientId);
            // check if status is valid
            _userInputValidation.ValidateDisbursedStatus(status);
            // check that start date is before end date
            _userInputValidation.ValidateDates(startDate, endDate);

            switch (metric)
            {
                case null:
                    var totalSumResult = _disbursedLoansTotal.CreateAppsDisbursedTotalAmountResponse(clientId, null, startDate, endDate);
                    return new OkObjectResult(totalSumResult);
                case MetricTypes.Total:
                    var totalSumResult2 = _disbursedLoansTotal.CreateAppsDisbursedTotalAmountResponse(clientId, null, startDate, endDate);
                    return new OkObjectResult(totalSumResult2);
                case MetricTypes.Monthly:
                    var totalMonthlyResult = _disbursedLoansMT.CreateAppsDisbursedMonthlyTotalsResponses(clientId, null, startDate, endDate);
                    return new OkObjectResult(totalMonthlyResult);
                case MetricTypes.FinanceCharges:
                    var productTypesResult = _disbursedLoansFC.CreateAppsDisbursedFinanceChargesResponse(clientId, null, startDate, endDate);
                    return new OkObjectResult(productTypesResult);

                default:
                    return new BadRequestObjectResult("Invalid metric specified.");
            }
        }

        public IActionResult GetDisbursedSumByAppIdResult(
            int clientId,
            StatusTypes status,
            MetricTypes? metric,
            long? applicationId)
        {
            // check if client Id and application Id are valid
            _userInputValidation.ValidateIds(clientId, applicationId);
            // check if status is valid
            _userInputValidation.ValidateDisbursedStatus(status);

            switch (metric)
            {
                case null:
                    var totalSumResult = _disbursedLoansTotal.CreateAppsDisbursedTotalAmountResponse(clientId, applicationId);
                    return new OkObjectResult(totalSumResult);
                case MetricTypes.Total:
                    var totalSumResult2 = _disbursedLoansTotal.CreateAppsDisbursedTotalAmountResponse(clientId, applicationId);
                    return new OkObjectResult(totalSumResult2);
                case MetricTypes.Monthly:
                    var totalMonthlyResult = _disbursedLoansMT.CreateAppsDisbursedMonthlyTotalsResponses(clientId, applicationId);
                    return new OkObjectResult(totalMonthlyResult);
                case MetricTypes.FinanceCharges:
                    var totalChargesResult = _disbursedLoansFC.CreateAppsDisbursedFinanceChargesResponse(clientId, applicationId);
                    return new OkObjectResult(totalChargesResult);

                default:
                    return new BadRequestObjectResult("Invalid metric specified.");
            }
        }
    }
}
