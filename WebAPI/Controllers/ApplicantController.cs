using Data.Validations;
using Microsoft.AspNetCore.Mvc;
using Services.Enums;
using Services.Services.ApplicationsParameters;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("applicants")]
    public class ApplicantController : ApplicationsBaseController
    {
        protected ApplicantsService _applicantService;
        public ApplicantController(
           ClientIdsValidation clientIdValidation,
           ApplicationIdsValidation applicationIdsValidation,
           MetricService metricService,
           ApplicantsService applicantsService)
           : base(metricService)
        {
            _applicantService = applicantsService;
        }

        /// <summary>
        /// retrieve all loans submitted on time deconstructed by single vs joint applicants
        /// </summary>
        /// <param name="clientId">1,2,3,4,7</param>
        /// <returns></returns>
        [HttpGet("")]
        public IActionResult GetAppTypes([FromHeader] int clientId, [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            return _applicantService.GetApplicantResult(clientId, startDate, endDate);
        }

        /// <summary>
        /// retrieve loans submitted on time deconstructed by a specific applicationId
        /// </summary>
        /// <param name="clientId">1,2,3,4,7</param>
        /// <param name="applicationId">12051874-12052927</param>
        /// <returns></returns>
        [HttpGet("{applicationId:long?}")]
        public IActionResult GetAppTypes([FromHeader] int clientId, long? applicationId)
        {
            return _applicantService.GetApplicantByAppIdResult(clientId, applicationId);
        }

        /// <summary>
        /// retrieves all loans deconstructed by applicant type and credit decisions or product types
        /// </summary>
        /// <param name="clientId">1,2,3,4,7</param>
        /// <param name="metric">null, creditDecisions or productTypes</param>
        /// <returns></returns>
        [HttpGet("count")]
        public IActionResult GetGrouping(
            [FromHeader] int clientId,
            [FromQuery] MetricTypes? metric = null,
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null)
        {
            return _metricService.GetApplicantCountResult(clientId, metric, startDate, endDate);
        }

        /// <summary>
        /// retrieves all loans deconstructed by applicant type and credit decisions or product types of a specific applicationId
        /// </summary>
        /// <param name="clientId">1,2,3,4,7</param>
        /// <param name="applicationId">12051874-12052927</param>
        /// <param name="metric">creditDecisions or productTypes</param>
        /// <returns></returns>
        [HttpGet("{applicationId:long?}/find")]
        public IActionResult GetGrouping(
            [FromHeader] int clientId,
            [FromQuery, Required] MetricTypes metric,
            long? applicationId = null)
        {
            return _metricService.GetApplicantFindResult(clientId, metric, applicationId);
        }
    }
}