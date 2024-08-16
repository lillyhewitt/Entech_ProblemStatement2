
namespace Services.Responses.Applicants
{
    public class ApplicationsDataByApplicantTypeResponse
    {
        public long ApplicationId { get; set; }
        public string IsJointApplicant { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateOnly? WithdrawDate { get; set; }
        public DateOnly? CreditExpirationDate { get; set; }
        public DateOnly? OfferExpirationDate { get; set; }
        public DateOnly? FinalApprovalDate { get; set; }
        public DateOnly? RightToCancelDate { get; set; }
    }
}
