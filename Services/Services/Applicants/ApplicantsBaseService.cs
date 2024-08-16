using Microsoft.AspNetCore.Http;
using Services.Services.Applications;
using Data.Database;
using Data.Tables;

namespace Services.Services.Applicants
{
    public abstract class ApplicantsBaseService : ApplicationsBaseService
    {
        protected readonly ApplicationsDataByApplicantTypeService _applicantType;

        protected ApplicantsBaseService(
            AppContext dbContext,
            IHttpContextAccessor httpContextAccessor,
            ApplicationsDataByApplicantTypeService applicantTypeService)
            : base(dbContext, httpContextAccessor)
        {
            _applicantType = applicantTypeService;
        }

        // get the applicant type for a specific application
        protected string? GetApplicantType(int clientId, long? applicationId, DateTime? startDate, DateTime? endDate)
        {
            return _applicantType.CreateAppsDataByApplicantTypeResponses(clientId, applicationId, startDate, endDate).FirstOrDefault()?.IsJointApplicant;
        }

        // check if dates are provided, narrow query search if so
        protected IQueryable<Application> FilterClientId(int clientId)
        {
            var query = _dbContext.Applications
                                  .Where(app => app.Applicants.Any(a => a.ClientId == clientId));

            return query;
        }
    }
}
