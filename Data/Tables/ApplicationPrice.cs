namespace Data.Tables;

/// <summary>
/// Interest accrued during the loan grace period.
/// </summary>
public partial class ApplicationPrice
{
    public int Id { get; set; }

    public int ClientId { get; set; }

    public long? EntityRolesId { get; set; }

    public int? OwnedById { get; set; }

    public int ApplicationId { get; set; }

    public int? OfferId { get; set; }

    public long? PriceId { get; set; }

    public decimal? Apr { get; set; }

    public decimal? Dti { get; set; }

    public int? NumberOfPayments { get; set; }

    /// <summary>
    /// Monthly payment due during the loan grace period.
    /// </summary>
    public decimal? GraceMonthlyPayment { get; set; }

    /// <summary>
    /// Monthly payment due during the loan deferment period.
    /// </summary>
    public decimal? DeferredMonthlyPayment { get; set; }

    public decimal? TotalMonthlyPayment { get; set; }

    public decimal? LastMonthlyPayment { get; set; }

    public decimal? TotalLoan { get; set; }

    /// <summary>
    /// Interest accrued during the loan grace period.
    /// </summary>
    public decimal? GraceInterestAccrual { get; set; }

    public decimal? DeferredInterestAccrual { get; set; }

    public int ApplicationPriceStatusId { get; set; }

    public DateOnly? FirstPaymentDate { get; set; }

    public decimal? CustomDeferredInterest { get; set; }

    public DateOnly? CustomDeferredInterestEndDate { get; set; }

    public string? InterestRateTypeCode { get; set; }

    public string? RepaymentTypeCode { get; set; }

    public int? Term { get; set; }

    public decimal? MarginRate { get; set; }

    public decimal? IndexedRate { get; set; }

    public decimal? FixedRate { get; set; }

    public decimal? CalculatedRate { get; set; }

    public DateTime? SelectedDate { get; set; }

    public DateTime CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public int? DeletedBy { get; set; }

    public ulong Active { get; set; }

    public virtual Application Application { get; set; } = null!;
}