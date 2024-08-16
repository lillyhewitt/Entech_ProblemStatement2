
namespace Services.QueryResults.Applications
{
    public class ApplicationsByCreditDecisionsResult
    {
        public long ApplicationId { get; set; }
        public string? ProviderDecisionDescription { get; set; }
        public int TotalNum { get; set; }
    }
}
