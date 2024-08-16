namespace Data.Tables;

public partial class DisbursementStateManagement
{
    public int Id { get; set; }

    public int ClientId { get; set; }

    public long? EntityRolesId { get; set; }

    public int? OwnedById { get; set; }

    public int DisbursementStateId { get; set; }

    public DateTime CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public int? DeletedBy { get; set; }

    public ulong Active { get; set; }

    public virtual ProcessState DisbursementState { get; set; } = null!;

    public virtual ApplicationDisbursement IdNavigation { get; set; } = null!;
}
