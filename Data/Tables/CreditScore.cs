namespace Data.Tables;

public partial class CreditScore
{
    public int Id { get; set; }

    public int ClientId { get; set; }

    public long? EntityRolesId { get; set; }

    public int? OwnedById { get; set; }

    public int CreditResponseId { get; set; }

    public int CreditScoreTypeId { get; set; }

    /// <summary>
    /// Value pulled from credit response for each score.
    /// </summary>
    public int? Value { get; set; }

    /// <summary>
    /// Credit Score Percentile pulled from credit response for each score.
    /// </summary>
    public decimal? Percentile { get; set; }

    /// <summary>
    /// Value pulled from credit response.
    /// </summary>
    public ulong? IsPassed { get; set; }

    /// <summary>
    /// Value pulled from credit response.
    /// </summary>
    public ulong? IsFactActFound { get; set; }

    /// <summary>
    /// Code returned from credit for a score.factact record.
    /// </summary>
    public string? FactActCode { get; set; }

    public DateTime CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public virtual CreditResponse CreditResponse { get; set; } = null!;
}
