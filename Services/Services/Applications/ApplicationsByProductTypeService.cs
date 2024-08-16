using Microsoft.AspNetCore.Http;
using Services.Interfaces.Applications;
using Services.QueryResults.Applications;
using Services.Responses.Applications;
using Data.Database;

namespace Services.Services.Applications
{
    public sealed class ApplicationsByProductTypeService : ApplicationsBaseService, IApplicationsByProductTypeService
    {
        // create instance of AppContext (database)
        public ApplicationsByProductTypeService(AppContext dbContext, IHttpContextAccessor httpContextAccessor) : base(dbContext, httpContextAccessor) { }

        // Count the number of applications per product type that matches the applicationId or all applications
        public List<ApplicationsByProductTypeResult> QueryAppsByProductType(
            int clientId,
            long? applicationId,
            DateTime? startDate,
            DateTime? endDate)
        {
            // initial query to filter by clientId
            var queryFiltered = _dbContext.Applications
                                .Join(_dbContext.Products,
                                    application => application.ProductId,
                                    product => product.Id,
                                    (a, p) => new
                                    {
                                        p.DisplayName,
                                        a.ClientId,
                                        a.Id,
                                        a.CreatedAt
                                    })
                                .Where(grouping => grouping.ClientId == clientId);

            // filters by applicationId
            var filteredQuery = FilterByApplicationId(queryFiltered, applicationId, item => item.Id);

            // filters by dates
            filteredQuery = FilterByDates(filteredQuery, startDate, endDate, item => item.CreatedAt);

            // group by DisplayName and select the results
            var groupedQuery = filteredQuery
                            .GroupBy(grouping => grouping.DisplayName)
                            .Select(grouping => new ApplicationsByProductTypeResult
                            {
                                DisplayName = grouping.Key,
                                TotalNum = grouping.Select(a => a.Id).Distinct().Count()
                            });

            // Return the results from the query
            return groupedQuery.ToList(); ;
        }

        // create list of repsonses for number of applications per product type
        public List<ApplicationsByProductTypeResponse> CreateAppsByProductTypeResponses(
            int clientId,
            long? applicationId = null,
            DateTime? startDate = null,
            DateTime? endDate = null)
        {
            // run the query
            var queryResult = QueryAppsByProductType(clientId, applicationId, startDate, endDate);

            // create list of responses 
            var productTypeResponseList = queryResult
                .Select(item => new ApplicationsByProductTypeResponse
                {
                    ProductType = item.DisplayName,
                    Number = item.TotalNum
                })
                .ToList();

            return productTypeResponseList;
        }

        // create list of repsonses for the product type of a specific application
        public List<ApplicationByProductTypeResponse> CreateAppByProductTypeResponses(
            int clientId,
            long? applicationId = null,
            DateTime? startDate = null,
            DateTime? endDate = null)
        {
            // run the query
            var queryResult = QueryAppsByProductType(clientId, applicationId, startDate, endDate);

            // create list of responses 
            var productTypeResponseList = queryResult
                .Select(item => new ApplicationByProductTypeResponse
                {
                    ProductType = item.DisplayName
                })
                .ToList();

            return productTypeResponseList;
        }
    }
}
