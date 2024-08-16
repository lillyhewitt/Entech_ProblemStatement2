using Microsoft.AspNetCore.Mvc;
using Services.Services.ApplicationsParameters;

namespace WebAPI.Controllers
{
    public abstract class ApplicationsBaseController : ControllerBase
    {
        protected MetricService _metricService;

        public ApplicationsBaseController(
            MetricService metricService)
        {
            _metricService = metricService;
        }
    }
}