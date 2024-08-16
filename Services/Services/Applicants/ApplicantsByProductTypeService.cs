using Microsoft.AspNetCore.Http;
using Services.Interfaces.Applicants;
using Services.QueryResults.Applicants;
using Services.QueryResults.Applications;
using Services.Responses.Applicants;
using Services.Services.Applications;
using Data.Database;

namespace Services.Services.Applicants
{
    public sealed class ApplicantsByProductTypeService : ApplicantsBaseService, IApplicantsByProductTypeService
    {
        // make database instance 
        public ApplicantsByProductTypeService(
            AppContext dbContext,
            IHttpContextAccessor httpContextAccessor,
            ApplicationsDataByApplicantTypeService applicantTypeService)
            : base(dbContext, httpContextAccessor, applicantTypeService) { }

        // counts the total number of applicant types per product type
        protected List<ApplicantsByProductTypeResult> QueryAppsByProductType(int clientId, DateTime? startDate, DateTime? endDate)
        {
            // run initial query (filter by clientId)
            var filteredQuery = FilterClientId(clientId);

            // narrow query search if dates are provided
            filteredQuery = FilterByDates(filteredQuery, startDate, endDate, application => application.CreatedAt);

            // group by applicationId and select the results
            var groupedQuery = filteredQuery
                .GroupBy(application => new
                {
                    application.ProductId,
                    ApplicantCount = application.Applicants.Count(applicant => applicant.ClientId == clientId)
                })
                .Select(grouping => new
                {
                    grouping.Key.ProductId,
                    TotalNum = grouping.Key.ApplicantCount
                })
                .Join(_dbContext.Products,
                    application => application.ProductId,
                    product => product.Id,
                    (a, p) => new
                    {
                        p.DisplayName,
                        a.TotalNum
                    })
                .GroupBy(grouping => grouping.DisplayName)
                .Select(grouping => new ApplicantsByProductTypeResult
                {
                    DisplayName = grouping.Key,
                    CountSingleApplicants = grouping.Count(y => y.TotalNum == 1),
                    CountJointApplicants = grouping.Count(y => y.TotalNum == 2)
                });

            // return the results of the query
            return groupedQuery.ToList();
        }

        // create list of responses of the number of each product type per applicant type
        public List<ApplicantsByProductTypeResponse> CreateAppsByProductTypeResponses(int clientId, DateTime? startDate = null, DateTime? endDate = null)
        {
            // run the query
            var queryOfTotalsPerProductType = QueryAppsByProductType(clientId, startDate, endDate);

            // create a list of responses
            var productTypeList = queryOfTotalsPerProductType
              .Select(item => new ApplicantsByProductTypeResponse
              {
                  Title = item.DisplayName,
                  SingleApplicants = item.CountSingleApplicants,
                  JointApplicants = item.CountJointApplicants
              })
              .ToList();

            return productTypeList;
        }

        // counts the product type of a specific applicant
        protected List<ApplicationProductTypeByApplicantResult> QueryAppByProductType(int clientId, long? applicationId)
        {
            // find if applicant is a single or joint applicant
            var applicantType = GetApplicantType(clientId, applicationId, null, null);

            // queries to get the number of applications per product type
            var product = _dbContext.Applications
                        .Where(application => application.Applicants.Any(applicant => applicant.ClientId == clientId)
                                    && application.Id == applicationId)
                        .Join(_dbContext.Products,
                            application => application.ProductId,
                            product => product.Id,
                            (a, p) => new
                            {
                                p.DisplayName
                            })
                        .GroupBy(grouping => grouping.DisplayName)
                        .Select(grouping => new ApplicationProductTypeByApplicantResult
                        {
                            DisplayName = grouping.Key,
                            IsJointApplicant = applicantType
                        });

            // return the results from the query
            return product.ToList();
        }

        // create list of responses of a specific applicant
        public List<ApplicantByProductTypeResponse> CreateAppByProductTypeResponses(int clientId, long? applicationId = null)
        {
            // run the query
            var queryOfIndivProductType = QueryAppByProductType(clientId, applicationId);

            // create a list of responses
            var productTypeList = queryOfIndivProductType
                 .Select(item => new ApplicantByProductTypeResponse
                 {
                     DisplayName = item.DisplayName,
                     IsJointApplicant = item.IsJointApplicant
                 })
                 .ToList();

            return productTypeList;
        }
    }
}
