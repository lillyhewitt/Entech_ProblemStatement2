namespace Data.Tables;

/// <summary>
/// Employee Id to identify the employee who entered the application of behalf of the customer.
/// </summary>
public partial class Applicant
{
    public int Id { get; set; }

    public int ClientId { get; set; }

    public long? EntityRolesId { get; set; }

    public int? OwnedById { get; set; }

    public int UserProfileId { get; set; }

    public int BorrowerTypeId { get; set; }

    public int CitizenshipTypeId { get; set; }

    /// <summary>
    /// Branch Id to identify the branch that the application was entered at.
    /// </summary>
    public int? BranchId { get; set; }

    /// <summary>
    /// Employee Id to identify the employee who entered the application of behalf of the customer.
    /// </summary>
    public int? EmployeeId { get; set; }

    public string? Ssn { get; set; }

    public string? SsnlastFour { get; set; }

    public string? AlienId { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public DateTime CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public int? DeletedBy { get; set; }

    public ulong Active { get; set; }

    public ulong? Mla { get; set; }

    public virtual ICollection<CreditResponse> CreditResponses { get; set; } = new List<CreditResponse>();

    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();

    public virtual ICollection<Income> Incomes { get; set; } = new List<Income>();
}