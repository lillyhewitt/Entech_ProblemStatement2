namespace Data.Tables;

public partial class CreditDecision
{
    public int Id { get; set; }

    public int ClientId { get; set; }

    public long? EntityRolesId { get; set; }

    public int? OwnedById { get; set; }

    public int ApplicationId { get; set; }

    public int? ProviderDecision { get; set; }

    public string? ProviderDecisionDescription { get; set; }

    public int? SystemDecisionTypeId { get; set; }

    public int? UnderwriterDecisionTypeId { get; set; }

    public DateTime CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public int? DeletedBy { get; set; }

    public ulong Active { get; set; }

    public virtual Application Application { get; set; } = null!;
}
