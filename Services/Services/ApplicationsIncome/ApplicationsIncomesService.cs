using Microsoft.EntityFrameworkCore;
using Services.Interfaces.ApplicationsIncome;
using Services.Responses.ApplicationsIncome;
using Services.Services.ApplicationsDisbursed;
using Microsoft.AspNetCore.Http;
using Data.Database;
using Data.Validations;
using Data.Tables;

namespace Services.Services.ApplicationsIncome
{
    public sealed class ApplicationsIncomesService : ApplicationsDisbursedBaseService, IApplicationsIncomesService
    {
        // create variables to store average and median
        private decimal average;
        private decimal median;

        // make database instance 
        public ApplicationsIncomesService(
            AppContext dbContext,
            IHttpContextAccessor httpContextAccessor,
            DisbursementStateIdsValidation disbursementStateIds)
            : base(dbContext, httpContextAccessor, disbursementStateIds) { }

        // query of all loans that takes out entries with the wrong client id and/or incomes
        protected IQueryable<Income> ApplicationsQuery(int clientId)
        {
            // find the mean credit score for disbursed loans
            var query = _dbContext.Incomes
                      .Where(income => income.ClientId == clientId && income.Amount > 0.0m)
                      .Select(income => income); ;

            return query;
        }

        // query of disbursed loans that takes out entries with the wrong client id and/or incomes
        protected IQueryable<Income> ApplicationsDisbursedQuery(int clientId)
        {
            // find the mean credit score for disbursed loans
            var query = _dbContext.Applications
                        .Include(a => a.Applicants)
                        .SelectMany(a => a.Applicants, (a, aa) => new { Application = a, Applicant = aa })
                        .Join(_dbContext.ApplicationConsolidations,
                              a => a.Application.Id,
                              ac => ac.ApplicationId,
                              (a, ac) => new { a.Application, a.Applicant, ac })
                        .Join(_dbContext.DisbursementStateManagements,
                              aac => aac.ac.ApplicationDisbursementId,
                              dsm => dsm.Id,
                              (aac, dsm) => new { aac.Application, aac.Applicant, aac.ac, dsm })
                        .Join(_dbContext.ApplicationDisbursements,
                              aacdsm => aacdsm.ac.ApplicationId,
                              ad => ad.ApplicationId,
                              (aacdsm, ad) => new { aacdsm.Application, aacdsm.Applicant, aacdsm.ac, aacdsm.dsm, ad })
                        .Join(_dbContext.Incomes,
                              aacdsmi => aacdsmi.Applicant.Id,
                              i => i.Id,
                              (aacdsmi, i) => new { aacdsmi.Application, aacdsmi.Applicant, aacdsmi.ac, aacdsmi.dsm, aacdsmi.ad, i })
                        .Where(aacdsmi => aacdsmi.dsm.DisbursementStateId != null
                                           && RetrieveDisbursementStateIds().Contains(aacdsmi.dsm.DisbursementStateId)
                                           && aacdsmi.i.ClientId == clientId
                                           && aacdsmi.ad.DisbursedDate != null
                                           && aacdsmi.i.Amount > 0)
                        .Select(aacdsmi => aacdsmi.i);

            return query;
        }

        // group values by applicationId  and select the first value associated with applicationId
        protected List<decimal> GroupIncomeByApplicationId(IQueryable<Income> query)
        {
            // group by ApplicationId and select the first interest rate in each group
            var groupedValues = query
                                .GroupBy(income => income.Applicants.FirstOrDefault().Applications.FirstOrDefault().Id)
                                .Select(group => group.First().Amount);

            return groupedValues.ToList();
        }

        // queries data then finds the mean and media credit scores for all or disbursed loans
        protected void CollectMeanAndMedian(
            IQueryable<Income> query,
            decimal? startRange = null,
            decimal? endRange = null,
            DateTime? startDate = null,
            DateTime? endDate = null)
        {
            // filter query by income ranges
            var filteredIncomes = FilterByRanges(query, startRange, endRange, income => income.Amount);

            // filters by dates
            filteredIncomes = FilterByDates(filteredIncomes, startDate, endDate, income => income.CreatedAt);

            // group incomes by application ID
            var groupedIncomes = GroupIncomeByApplicationId(filteredIncomes);

            // get the average and median income from the queries
            average = groupedIncomes.Average();
            median = CalculateMedian(groupedIncomes);
        }

        // lists the average and median income for applicants
        public ApplicationsIncomesResponse CreateMeanAndMedianAppsIncomesResponse(
            int clientId,
            decimal? startRange = null,
            decimal? endRange = null,
            DateTime? startDate = null,
            DateTime? endDate = null)
        {
            // query finding entries with matching client ids and valid credit scores
            var query = ApplicationsQuery(clientId);

            // calculate mean and median and update private variables
            CollectMeanAndMedian(query, startRange, endRange, startDate, endDate);

            // return the results (mean and median income)
            var response = new ApplicationsIncomesResponse
            {
                Average = Math.Round(average, 2),
                Median = Math.Round(median, 2)
            };

            return response;
        }

        // lists the average and median income for disbursed loans
        public ApplicationsDisbursedIncomeResponse CreateMeanAndMedianAppsDisbursedIncomeResponse(
            int clientId,
            decimal? startRange = null,
            decimal? endRange = null,
            DateTime? startDate = null,
            DateTime? endDate = null)
        {
            // query finding entries with matching client ids and valid credit scores
            var query = ApplicationsDisbursedQuery(clientId);

            // calculate mean and median and update private variables
            CollectMeanAndMedian(query, startRange, endRange, startDate, endDate);

            // create a response from the query results (mean and median income for disbursed loans)
            var response = new ApplicationsDisbursedIncomeResponse
            {
                Average = Math.Round(average, 2),
                Median = Math.Round(median, 2)
            };

            return response;
        }
    }
}
