using Data.Validations;
using Microsoft.AspNetCore.Mvc;
using Services.Enums;
using Services.Services.ApplicationsParameters;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("applications")]
    public class AllApplicationsController : ApplicationsBaseController
    {
        public AllApplicationsController(
            ClientIdsValidation clientIdValidation,
            ApplicationIdsValidation applicationIdsValidation,
            MetricService metricService
        ) : base(metricService) { }

        /// <summary>
        /// retrieves all loans deconstructed by credit decisions, application statuses, or product types
        /// </summary>
        /// <param name="clientId">1,2,3,4,7</param>
        /// <param name="metric">null,all,creditDecisions, applicationStatuses, or productTypes</param>
        /// <returns></returns>
        [HttpGet("{status}/count")]
        // create Enum for metric and status 
        public IActionResult GetGrouping(
            [FromHeader] int clientId,
            StatusTypes status,
            [FromQuery] MetricTypes? metric = null,
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null)
        {
            return _metricService.GetStatusCountResult(clientId, status, metric, startDate, endDate);
        }

        /// <summary>
        /// retrieves all loans deconstructed by credit decisions, application statuses, or product types of a specific applicationId
        /// </summary>
        /// <param name="clientId">1,2,3,4,7</param>
        /// <param name="applicationId">12051874-12052927</param>
        /// <param name="metric">null,all,creditDecisions, applicationStatuses, or productTypes</param>
        /// <returns></returns>
        [HttpGet("{status}/{applicationId:long?}/find")]
        public IActionResult GetGrouping(
            [FromHeader] int clientId,
            StatusTypes status,
            long? applicationId,
            [FromQuery] MetricTypes? metric)
        {
            return _metricService.GetStatusFindResult(clientId, status, metric, applicationId);
        }
    }
}