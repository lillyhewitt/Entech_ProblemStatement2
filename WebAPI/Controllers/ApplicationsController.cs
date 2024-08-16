using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Services.Enums;
using Services.Services.ApplicationsParameters;
using Data.Validations;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("applications")]
    public class ApplicationsController : ApplicationsBaseController
    {
        protected StatusService _statusService;

        public ApplicationsController(
            ClientIdsValidation clientIdValidation,
            ApplicationIdsValidation applicationIdsValidation,
            MetricService metricService,
            StatusService statusService
        ) : base(metricService)
        {
            _statusService = statusService;
        }

        /// <summary>
        /// retrieves  data of loans, all or disbursed 
        /// </summary>
        /// <param name="clientId">1,2,3,4,7</param>
        /// <param name="status">all or disbursed</param>
        /// <returns></returns>
        [HttpGet("{status}")]
        public IActionResult GetLoans(
            [FromHeader] int clientId,
            StatusTypes status,
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate)
        {
            return _statusService.GetStatusResult(clientId, status, startDate, endDate);
        }

        /// <summary>
        /// retrieves data of loans matching an applicationId, all or disbursed 
        /// </summary>
        /// <param name="clientId">1,2,3,4,7</param>
        /// <param name="applicationId">12051874-12052927</param>
        /// <param name="status">"All or Disbursed</param>
        /// <returns></returns>
        [HttpGet("{status}/{applicationId:long?}")]
        public IActionResult GetLoans([FromHeader] int clientId, StatusTypes status, long? applicationId)
        {
            return _statusService.GetStatusByAppIdResult(clientId, status, applicationId);
        }

        /// <summary>
        /// retrieves the mean and median of metrics for loans, all or disbursed
        /// </summary>
        /// <param name="clientId">1,2,3,4,7</param>
        /// <param name="status">all or disbursed</param>
        /// <param name="metric">All: creditScore, income.   Disbursed: apr, creditScore, dti, income, or interestRate.</param>
        /// <returns></returns>
        // DISBURSED: INCOME
        [HttpGet("{status}/mean-and-median")]
        public IActionResult GetLoans(
            [FromHeader] int clientId,
            StatusTypes status,
            [FromQuery, Required] MetricTypes metric,
            [FromQuery] decimal? startRange,
            [FromQuery] decimal? endRange,
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate)
        {
            return _statusService.GetMeanAndMedianResult(clientId, status, metric, startRange, endRange, startDate, endDate);
        }
    }
}