using Services.Responses.Applications;
using Services.Responses.CreditScore;

namespace Services.Interfaces.CreditScore
{
    public interface IApplicationsCreditScoresService
    {
        public ApplicationsCreditScoresResponse CreateMeanAndMedianAppsResponse(int clientId, decimal? startRange = null, decimal? endRange = null, DateTime? startDate = null, DateTime? endDate = null);
        public ApplicationsAveragesResponse CreateMeanAndMedianAppsDisbursedResponse(int clientId, decimal? startRange = null, decimal? endRange = null, DateTime? startDate = null, DateTime? endDate = null);
    }
}
