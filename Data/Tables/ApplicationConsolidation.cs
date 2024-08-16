namespace Data.Tables;
public partial class ApplicationConsolidation
{
    public int Id { get; set; }

    public int ClientId { get; set; }

    public long? EntityRolesId { get; set; }

    public int? OwnedById { get; set; }

    public int ApplicationId { get; set; }

    public int? ApplicationDisbursementId { get; set; }

    public string SelectedCreditorName { get; set; } = null!;

    public int? CreditorProfileId { get; set; }

    public int? LoanLineIndex { get; set; }

    public string? LoanSequenceNumber { get; set; }

    public string? AccountNumber { get; set; }

    public decimal? InterestRate { get; set; }

    public int EstimatedLoanBalance { get; set; }

    public decimal? ConfirmedLoanBalance { get; set; }

    public decimal? MonthlyPayment { get; set; }

    public decimal? CalculatedPayoffAmount { get; set; }

    public ulong IsFederal { get; set; }

    public DateOnly? StatementPaymentDate { get; set; }

    public long? DocumentId { get; set; }

    public DateTime CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public int? DeletedBy { get; set; }

    public ulong Active { get; set; }

    public virtual Application Application { get; set; } = null!;

    public virtual ApplicationDisbursement? ApplicationDisbursement { get; set; }
}