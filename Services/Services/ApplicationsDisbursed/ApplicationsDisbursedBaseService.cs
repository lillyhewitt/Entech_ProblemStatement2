using Services.Services.Applications;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using Data.Database;
using Data.Validations;

namespace Services.Services.ApplicationsDisbursed
{
    public abstract class ApplicationsDisbursedBaseService : ApplicationsBaseService
    {
        private readonly DisbursementStateIdsValidation _disbursementStateIds;

        protected ApplicationsDisbursedBaseService(
            AppContext dbContext,
            IHttpContextAccessor httpContextAccessor,
            DisbursementStateIdsValidation disbursementStateIds) : base(dbContext, httpContextAccessor)
        {
            _disbursementStateIds = disbursementStateIds;
        }

        // returns a list of disbursement state ids
        protected List<int> RetrieveDisbursementStateIds()
        {
            return _disbursementStateIds.GetDisbursementStateIds();
        }

        // calculates the median from a list of sorted values
        protected decimal CalculateMedian(List<decimal> unsortedValues)
        {
            // sort the values in ascending order
            var sortedValues = unsortedValues.OrderBy(value => value).ToList();

            // count the number of entries in the list
            int count = sortedValues.Count();
            if (count == 0)
            {
                return 0;
            }
            // find mean of middle entries if list is even
            else if (count % 2 == 0)
            {
                return (sortedValues[count / 2 - 1] + sortedValues[count / 2]) / 2.0m;
            }
            // find middle entry is list is odd
            else
            {
                return sortedValues[count / 2];
            }
        }

        // filters queries by value and date ranges
        protected IQueryable<T> FilterByRanges<T>(
            IQueryable<T> query,
            decimal? startRange,
            decimal? endRange,
            Expression<Func<T, decimal?>> rangeSelector)
        {
            // check if value ranges are provided, narrow search if so
            if (startRange != null)
            {
                var startRangeExpression = Expression.Lambda<Func<T, bool>>(
                                              Expression.GreaterThanOrEqual(
                                                  rangeSelector.Body,
                                                  Expression.Constant(startRange, typeof(decimal?))),
                                              rangeSelector.Parameters);

                query = query.Where(startRangeExpression);
            }
            if (endRange != null)
            {
                var endRangeExpression = Expression.Lambda<Func<T, bool>>(
                                              Expression.LessThanOrEqual(
                                                  rangeSelector.Body,
                                                  Expression.Constant(endRange, typeof(decimal?))),
                                              rangeSelector.Parameters);

                query = query.Where(endRangeExpression);
            }

            return query;
        }
    }
}
