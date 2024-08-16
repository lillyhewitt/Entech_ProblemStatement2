namespace Data.Tables;

/// <summary>
/// Pre-Disbursement Reinstated Amount
/// </summary>
public partial class ApplicationDisbursement
{
    public int Id { get; set; }

    public int ClientId { get; set; }

    public long? EntityRolesId { get; set; }

    public int? OwnedById { get; set; }

    public int DisbursementProfileId { get; set; }

    public int? DisbursingAgentId { get; set; }

    public int? DisbursementMethodTypeId { get; set; }

    public int DisbursementSegmentTypeId { get; set; }

    public int ApplicationId { get; set; }

    public int SegmentNumber { get; set; }

    public decimal? GrossAmount { get; set; }

    public decimal? NetAmount { get; set; }

    public decimal? RecommendedAmount { get; set; }

    public decimal? PreDisbCancelledAmount { get; set; }

    public decimal? PostDisbCancelledAmount { get; set; }

    /// <summary>
    /// Pre-Disbursement Reinstated Amount
    /// </summary>
    public decimal? PreDisbReinstatedAmount { get; set; }

    /// <summary>
    /// Post-Disbursement Reinstated Amount
    /// </summary>
    public decimal? PostDisbReinstatedAmount { get; set; }

    public decimal? OriginationFee { get; set; }

    public DateOnly? RecommendedDate { get; set; }

    public DateOnly? SchedulePreDisbursementDate { get; set; }

    public DateOnly? PreDisbursementDate { get; set; }

    public DateOnly? ScheduledDate { get; set; }

    public DateOnly? DisbursedDate { get; set; }

    public DateOnly? ServicerExtractDate { get; set; }

    public ulong IsEstimate { get; set; }

    /// <summary>
    /// A 1-character code from CL4 translated to a bit indicating if the previously cancelled disbursement is being reinstated. Y, reinstate previously cancelled disbursement, N = No, this is not a reinstatement
    /// </summary>
    public ulong? IsReinstated { get; set; }

    public int? CheckNumber { get; set; }

    public DateOnly? CheckStatusDate { get; set; }

    public string? PaidCode { get; set; }

    public DateOnly? PaidProcessDate { get; set; }

    public ulong? HoldFlag { get; set; }

    public DateTime CreatedAt { get; set; }

    public int CreatedById { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public int? ModifiedById { get; set; }

    public DateTime? DeletedAt { get; set; }

    public int? DeletedById { get; set; }

    public ulong Active { get; set; }

    public virtual Application Application { get; set; } = null!;

    public virtual ICollection<ApplicationConsolidation> ApplicationConsolidations { get; set; } = new List<ApplicationConsolidation>();

    public virtual DisbursementStateManagement? DisbursementStateManagement { get; set; }
}