
namespace Services.QueryResults.Applications
{
    public class ApplicationsDataByApplicantTypeResult
    {
        public long ApplicationId { get; set; }
        public string ApplicantType { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateOnly? WithdrawDate { get; set; }
        public DateOnly? CreditExpirationDate { get; set; }
        public DateOnly? OfferExpirationDate { get; set; }
        public DateOnly? FinalApprovalDate { get; set; }
        public DateOnly? RightToCancelDate { get; set; }
    }
}
