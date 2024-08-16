
namespace Services.QueryResults.Applicants
{
    public class ApplicantsByCreditDecisionResult
    {
        public string? ProviderDecisionDescription { get; set; }
        public int CountSingleApplicants { get; set; }
        public int CountJointApplicants { get; set; }
    }
}
