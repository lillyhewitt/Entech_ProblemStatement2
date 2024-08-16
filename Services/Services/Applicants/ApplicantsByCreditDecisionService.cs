using Data.Database;
using Services.QueryResults.Applicants;
using Services.QueryResults.Applications;
using Services.Responses.Applicants;
using Services.Interfaces.Applicants;
using Microsoft.AspNetCore.Http;
using Services.Services.Applications;

namespace Services.Services.Applicants
{
    public sealed class ApplicantsByCreditDecisionService : ApplicantsBaseService, IApplicantsByCreditDecisionService
    {
        // make database instance 
        public ApplicantsByCreditDecisionService(
            AppContext dbContext,
            IHttpContextAccessor httpContextAccessor,
            ApplicationsDataByApplicantTypeService applicantTypeService)
            : base(dbContext, httpContextAccessor, applicantTypeService) { }

        // counts the total number of applicant types per credit decision
        protected List<ApplicantsByCreditDecisionResult> QueryAppsByCreditDecision(int clientId, DateTime? startDate, DateTime? endDate)
        {
            // run initial query (filter by clientId)
            var filteredQuery = FilterClientId(clientId);

            // narrow query search if dates are provided
            filteredQuery = FilterByDates(filteredQuery, startDate, endDate, application => application.CreatedAt);

            // group by applicationId and select the results
            var groupedQuery = filteredQuery
                .GroupBy(application => new
                {
                    application.Id,
                    ApplicantCount = application.Applicants.Count(applicant => applicant.ClientId == clientId)
                })
                .Select(grouping => new
                {
                    ApplicationId = grouping.Key.Id,
                    TotalNum = grouping.Key.ApplicantCount
                })
                .Join(_dbContext.CreditDecisions,
                    application => application.ApplicationId,
                    creditDecision => creditDecision.ApplicationId,
                    (a, cd) => new
                    {
                        cd.ProviderDecisionDescription,
                        a.TotalNum
                    })
                .GroupBy(grouping => grouping.ProviderDecisionDescription)
                .Select(grouping => new ApplicantsByCreditDecisionResult
                {
                    ProviderDecisionDescription = grouping.Key,
                    CountSingleApplicants = grouping.Count(y => y.TotalNum == 1),
                    CountJointApplicants = grouping.Count(y => y.TotalNum == 2)
                });

            // return the results of the query
            return groupedQuery.ToList();
        }

        // list the number of each credit decision per applicant type
        public List<ApplicantsByCreditDecisionResponse> CreateAppsByCreditDecisionResponses(int clientId, DateTime? startDate = null, DateTime? endDate = null)
        {
            // run query 
            var queryOfTotalsPerCreditDecision = QueryAppsByCreditDecision(clientId, startDate, endDate);

            // create list of responses 
            var creditDecisionList = queryOfTotalsPerCreditDecision
              .Select(item => new ApplicantsByCreditDecisionResponse
              {
                  ProviderDecisionDescription = item.ProviderDecisionDescription,
                  SingleApplicants = item.CountSingleApplicants,
                  JointApplicants = item.CountJointApplicants
              })
              .ToList();

            return creditDecisionList;
        }

        // dinds and counts the credit decision of a specific application
        protected List<ApplicationCreditDecisinByApplicantTypeResult> QueryAppByCreditDecision(int clientId, long? applicationId)
        {
            // find if applicant is a single or joint applicant
            var applicantType = GetApplicantType(clientId, applicationId, null, null);

            // query to get the number of each credit decision for an application
            var decision = _dbContext.Applications
                        .Where(application => application.Applicants.Any(applicant => applicant.ClientId == clientId)
                                    && application.Id == applicationId)
                        .Join(_dbContext.CreditDecisions,
                            application => application.Id,
                            creditDecision => creditDecision.ApplicationId,
                            (a, cd) => new
                            {
                                cd.ProviderDecisionDescription
                            })
                        .GroupBy(grouped => grouped.ProviderDecisionDescription)
                        .Select(grouped => new ApplicationCreditDecisinByApplicantTypeResult
                        {
                            ProviderDecisionDescription = grouped.Key,
                            IsJointApplicant = applicantType
                        });

            return decision.ToList();
        }

        // list the credit decisions of a specific applicant
        public List<ApplicantByCreditDecisionResponse> CreateAppByCreditDecisionResponses(int clientId, long? applicationId = null)
        {
            // run query 
            var queryOfIndivCreditDecision = QueryAppByCreditDecision(clientId, applicationId);

            // create list of responses 
            var creditDecisionList = queryOfIndivCreditDecision
                 .Select(item => new ApplicantByCreditDecisionResponse
                 {
                     ProviderDecisionDescription = item.ProviderDecisionDescription,
                     IsJointApplicant = item.IsJointApplicant
                 })
                 .ToList();

            return creditDecisionList;
        }
    }
}
