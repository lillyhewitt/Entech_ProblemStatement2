
using Data.Database;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;

namespace Services.Services.Applications
{
    public abstract class ApplicationsBaseService
    {
        protected readonly AppContext _dbContext;
        protected readonly IHttpContextAccessor _httpContextAccessor;
        protected int clientId;
        protected int setClientId;

        protected ApplicationsBaseService(AppContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            // set database context
            _dbContext = dbContext;

            // set clientId from header
            if (httpContextAccessor.HttpContext != null && httpContextAccessor.HttpContext.Request.Headers.TryGetValue("clientId", out Microsoft.Extensions.Primitives.StringValues clientIdInputted))
            {
                if (int.TryParse(clientIdInputted, out int setClientId))
                {
                    clientId = setClientId;
                }
            }
        }

        // filters the query by a specified applicationId 
        protected IQueryable<T> FilterByApplicationId<T>(
            IQueryable<T> query,
            long? applicationId,
            Expression<Func<T, long>> applicationIdSelector)
        {
            // check if application ID is provided, narrow query search if so
            if (applicationId != null)
            {
                // create an expression to check if applicationId equals specified id
                var applicationIdExpression = Expression.Lambda<Func<T, bool>>(
                                    Expression.Equal(applicationIdSelector.Body,
                                    Expression.Constant(applicationId.Value)),
                                    applicationIdSelector.Parameters[0]
                                );

                // apply expression to only return entries with matching applicationId
                query = query.Where(applicationIdExpression);
            }

            return query;
        }

        // filters the query by dates 
        protected IQueryable<T> FilterByDates<T>(
            IQueryable<T> query,
            DateTime? startDate,
            DateTime? endDate,
            Expression<Func<T, DateTime>> dateSelector)
        {
            // check if date ranges are provided, narrow search if so
            if (startDate != null)
            {
                // create an expression to check if date is greater than or equal to start date
                var startDateExpression = Expression.Lambda<Func<T, bool>>(
                                            Expression.GreaterThanOrEqual(dateSelector.Body, Expression.Constant(startDate.Value)),
                                            dateSelector.Parameters);

                query = query.Where(startDateExpression);
            }
            if (endDate != null)
            {
                // create an expression to check if date is less than or equal to end date
                var endDateExpression = Expression.Lambda<Func<T, bool>>(
                                            Expression.LessThanOrEqual(dateSelector.Body, Expression.Constant(endDate.Value)),
                                            dateSelector.Parameters);

                query = query.Where(endDateExpression);
            }

            return query;
        }
    }
}
