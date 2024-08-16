
namespace Services.Responses.Applicants
{
    public class ApplicantsByCreditDecisionResponse
    {
        public string? ProviderDecisionDescription { get; set; }
        public int SingleApplicants { get; set; }
        public int JointApplicants { get; set; }
    }
}
