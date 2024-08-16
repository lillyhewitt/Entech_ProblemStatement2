namespace Data.Tables;

public partial class CreditResponse
{
    public int Id { get; set; }

    public int ClientId { get; set; }

    public long? EntityRolesId { get; set; }

    public int? OwnedById { get; set; }

    public int ApplicationId { get; set; }

    public int? ApplicantId { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public int BorrowerTypeId { get; set; }

    public int? CreditReportId { get; set; }

    public int? CreditResult { get; set; }

    public string? CreditResultDescription { get; set; }

    public int? ResidenceTypeId { get; set; }

    public int ProviderId { get; set; }

    public int? CreditScore { get; set; }

    public string? ScoreReason { get; set; }

    public decimal? Dti { get; set; }

    public decimal? FreeCashFlow { get; set; }

    public decimal? StatedMonthlyIncome { get; set; }

    public decimal? MonthlyRevolvingExpenses { get; set; }

    public decimal? MonthlyInstallmentExpenses { get; set; }

    public decimal? MonthlyHousingExpenses { get; set; }

    public decimal? MonthlyOpenExpenses { get; set; }

    public decimal? MonthlyLineOfCreditDebt { get; set; }

    public decimal? UnderwriterAdjustment { get; set; }

    public DateTime DecisionDate { get; set; }

    public ulong? Ofac { get; set; }

    public ulong? FraudFlag { get; set; }

    public DateTime CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public int? DeletedBy { get; set; }

    public ulong Active { get; set; }

    public virtual Applicant? Applicant { get; set; }

    public virtual Application Application { get; set; } = null!;

    public virtual ICollection<CreditScore> CreditScores { get; set; } = new List<CreditScore>();
}
