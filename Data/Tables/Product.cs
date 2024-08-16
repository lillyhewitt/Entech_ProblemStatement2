namespace Data.Tables;
public partial class Product
{
    public int Id { get; set; }

    public int ClientId { get; set; }

    public long? EntityRolesId { get; set; }

    public int ProductCategoryId { get; set; }

    public int? ProductGroupId { get; set; }

    public int LenderProfileId { get; set; }

    public string DisplayName { get; set; } = null!;

    public string? Description { get; set; }

    public string? Code { get; set; }

    public string? Icon { get; set; }

    public string? Link { get; set; }

    public string? ButtonText { get; set; }

    public ulong Disabled { get; set; }

    public DateTime CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public int? DeletedBy { get; set; }

    public ulong Active { get; set; }

    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();
}
