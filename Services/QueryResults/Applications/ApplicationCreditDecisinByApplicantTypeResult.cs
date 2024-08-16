
namespace Services.QueryResults.Applications
{
    public class ApplicationCreditDecisinByApplicantTypeResult
    {
        public long? ApplicationId { get; set; }
        public string? ProviderDecisionDescription { get; set; }
        public string? IsJointApplicant { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
