namespace Data.Tables;

public partial class ProcessState
{
    public int Id { get; set; }

    public int ClientId { get; set; }

    public long? EntityRolesId { get; set; }

    public int ProcessStateGroupId { get; set; }

    public string StatusName { get; set; } = null!;

    public string? DisplayName { get; set; }

    public string? Description { get; set; }

    public string Code { get; set; } = null!;

    public int ProcessStateTypeId { get; set; }

    public DateTime CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public int? DeletedBy { get; set; }

    public ulong Active { get; set; }

    public virtual ICollection<ApplicationStateManagement> ApplicationStateManagementApplicationStates { get; set; } = new List<ApplicationStateManagement>();

    public virtual ICollection<ApplicationStateManagement> ApplicationStateManagementDecisionStates { get; set; } = new List<ApplicationStateManagement>();

    public virtual ICollection<DisbursementStateManagement> DisbursementStateManagements { get; set; } = new List<DisbursementStateManagement>();
}
