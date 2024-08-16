using Data.Validations;
using Microsoft.AspNetCore.Mvc;
using Services.Enums;
using Services.Services.ApplicationsParameters;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("applications")]
    public class DisbursedApplicationsController : ApplicationsBaseController
    {
        public DisbursedApplicationsController(
            ClientIdsValidation clientIdValidation,
            ApplicationIdsValidation applicationIdsValidation,
            MetricService metricService
        ) : base(metricService)
        {
        }

        /// <summary>
        /// retrieves total disbursed loans by all time, monthly, or finance charges
        /// </summary>
        /// <param name="clientId">1,2,3,4,7</param>
        /// <param name="metric">total, monthly, financeCharges</param>
        /// <returns></returns>
        [HttpGet("{status}/sum")]
        public IActionResult GetGrouping(
            [FromHeader] int clientId,
            StatusTypes status,
            [FromQuery] MetricTypes? metric = null,
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null)
        {
            return _metricService.GetDisbursedSumResult(clientId, status, metric, startDate, endDate);
        }

        /// <summary>
        ///  retrieves total disbursed loans by all time, monthly, or finance charges of a specific application
        /// </summary>
        /// <param name="clientId">1,2,3,4,7</param>
        /// <param name="applicationId">12051895-12052927</param>
        /// <param name="metric">total, monthly, financeCharges</param>
        /// <returns></returns>
        [HttpGet("{status}/{applicationId:long?}/sum")]
        public IActionResult GetGrouping(
            [FromHeader] int clientId,
            StatusTypes status,
            long? applicationId,
            [FromQuery] MetricTypes? metric = null)
        {
            return _metricService.GetDisbursedSumByAppIdResult(clientId, status, metric, applicationId);
        }
    }
}