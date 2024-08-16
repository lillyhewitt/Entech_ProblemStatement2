
using Services.Responses.ApplicationsIncome;

namespace Services.Interfaces.ApplicationsIncome
{
    public interface IApplicationsIncomesService
    {
        public ApplicationsIncomesResponse CreateMeanAndMedianAppsIncomesResponse(int clientId, decimal? startRange = null, decimal? endRange = null, DateTime? startDate = null, DateTime? endDate = null);

        public ApplicationsDisbursedIncomeResponse CreateMeanAndMedianAppsDisbursedIncomeResponse(int clientId, decimal? startRange = null, decimal? endRange = null, DateTime? startDate = null, DateTime? endDate = null);
    }
}
