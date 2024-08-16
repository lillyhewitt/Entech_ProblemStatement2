namespace Data.Tables;

/// <summary>
/// Indicates the source of this application, e.g. Branch vs Apply
/// </summary>
public partial class Application
{
    public int Id { get; set; }

    public int ClientId { get; set; }

    public long? EntityRolesId { get; set; }

    public int? OwnedById { get; set; }

    public int ProductId { get; set; }

    public int? ServicerAssignmentId { get; set; }

    /// <summary>
    /// Indicates the source of this application, e.g. Branch vs Apply
    /// </summary>
    public int? ApplicationSourceTypeId { get; set; }

    public ulong? HasCosigner { get; set; }

    public string? ReferralUrl { get; set; }

    public DateOnly? WithdrawDate { get; set; }

    public DateOnly? CreditExpirationDate { get; set; }

    public DateOnly? OfferExpirationDate { get; set; }

    public DateOnly? FinalApprovalDate { get; set; }

    public DateOnly? RightToCancelDate { get; set; }

    public ulong? IsServicerExtract { get; set; }

    public string? CustomerTimeZone { get; set; }

    public DateTime CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public int? DeletedBy { get; set; }

    public ulong Active { get; set; }

    public ulong IsMembershipVerified { get; set; }

    public DateTime? ConformanceVerifiedDate { get; set; }

    public virtual ICollection<ApplicationConsolidation> ApplicationConsolidations { get; set; } = new List<ApplicationConsolidation>();

    public virtual ICollection<ApplicationDisbursement> ApplicationDisbursements { get; set; } = new List<ApplicationDisbursement>();

    public virtual ICollection<ApplicationPrice> ApplicationPrices { get; set; } = new List<ApplicationPrice>();

    public virtual ApplicationStateManagement? ApplicationStateManagement { get; set; }

    public virtual ICollection<CreditDecision> CreditDecisions { get; set; } = new List<CreditDecision>();

    public virtual ICollection<CreditResponse> CreditResponses { get; set; } = new List<CreditResponse>();

    public virtual Product Product { get; set; } = null!;

    public virtual ICollection<Applicant> Applicants { get; set; } = new List<Applicant>();
}