using Microsoft.AspNetCore.Http;
using Services.Interfaces.Applicants;
using Services.QueryResults.Applications;
using Services.Responses.Applicants;
using Data.Database;

namespace Services.Services.Applications
{
    public sealed class ApplicationsDataByApplicantTypeService : ApplicationsBaseService, IApplicationsDataByApplicantTypeService
    {
        // make database instance 
        public ApplicationsDataByApplicantTypeService(AppContext dbContext, IHttpContextAccessor httpContextAccessor) : base(dbContext, httpContextAccessor) { }

        // query to an application and what type of applicant they are
        protected List<ApplicationsDataByApplicantTypeResult> QueryAppsDataByApplicantType(int clientId, long? applicationId, DateTime? startDate, DateTime? endDate)
        {
            // query to find all applicants and what type they are
            var queryApplicantData = _dbContext.Applications
                                    .Where(application => application.Applicants.Any(applicant => applicant.ClientId == clientId))
                                    .Select(application => new
                                    {
                                        ApplicationId = application.Id,
                                        ApplicantCount = application.Applicants.Count(applicant => applicant.ClientId == clientId),
                                        application.CreatedAt,
                                        application.WithdrawDate,
                                        application.CreditExpirationDate,
                                        application.OfferExpirationDate,
                                        application.FinalApprovalDate,
                                        application.RightToCancelDate,
                                    });

            // filters by applicationId
            var filteredQuery = FilterByApplicationId(queryApplicantData, applicationId, item => item.ApplicationId);

            // filters by dates
            filteredQuery = FilterByDates(filteredQuery, startDate, endDate, item => item.CreatedAt);

            // determine is applicant is single or joint based on the occurrence of their application ID
            var findType = filteredQuery
                            .Select(item => new ApplicationsDataByApplicantTypeResult
                            {
                                ApplicationId = item.ApplicationId,
                                ApplicantType = item.ApplicantCount == 1 ? "False" : "True",
                                CreatedAt = item.CreatedAt,
                                WithdrawDate = item.WithdrawDate,
                                CreditExpirationDate = item.CreditExpirationDate,
                                OfferExpirationDate = item.OfferExpirationDate,
                                FinalApprovalDate = item.FinalApprovalDate,
                                RightToCancelDate = item.RightToCancelDate
                            }).ToList();

            return findType;
        }

        // create individual entries for list to display data
        public List<ApplicationsDataByApplicantTypeResponse> CreateAppsDataByApplicantTypeResponses(int clientId, long? applicationId = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            // run the query
            var queryOfAppData = QueryAppsDataByApplicantType(clientId, applicationId, startDate, endDate);

            // create list of responses from query (applicants data and their types)
            var appTypePerApplicantList = queryOfAppData
              .Select(item => new ApplicationsDataByApplicantTypeResponse
              {
                  ApplicationId = item.ApplicationId,
                  IsJointApplicant = item.ApplicantType,
                  CreatedAt = item.CreatedAt,
                  WithdrawDate = item.WithdrawDate,
                  CreditExpirationDate = item.CreditExpirationDate,
                  OfferExpirationDate = item.OfferExpirationDate,
                  FinalApprovalDate = item.FinalApprovalDate,
                  RightToCancelDate = item.RightToCancelDate
              })
              .ToList();

            return appTypePerApplicantList;
        }
    }
}
