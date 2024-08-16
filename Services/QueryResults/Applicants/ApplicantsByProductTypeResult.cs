
namespace Services.QueryResults.Applicants
{
    public class ApplicantsByProductTypeResult
    {
        public string? DisplayName { get; set; }
        public int CountSingleApplicants { get; set; }
        public int CountJointApplicants { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
