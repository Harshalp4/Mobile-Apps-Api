using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Bit2Sky.Bit2EHR.FinTrack;

/// <summary>
/// Represents an asset (property, investment, vehicle, etc.) in FinTrack
/// </summary>
[Table("FtAssets")]
public class FtAsset : FullAuditedEntity<Guid>, IMustHaveTenant
{
    public const int MaxClientIdLength = 64;
    public const int MaxNameLength = 128;
    public const int MaxTypeLength = 32;
    public const int MaxNotesLength = 512;

    public int TenantId { get; set; }

    /// <summary>
    /// Client-side unique identifier for sync
    /// </summary>
    [Required]
    [StringLength(MaxClientIdLength)]
    public string ClientId { get; set; }

    /// <summary>
    /// Asset name (e.g., "Apartment in Mumbai", "Gold Jewelry")
    /// </summary>
    [Required]
    [StringLength(MaxNameLength)]
    public string Name { get; set; }

    /// <summary>
    /// Asset type: property, vehicle, gold, investment, other
    /// </summary>
    [Required]
    [StringLength(MaxTypeLength)]
    public string AssetType { get; set; }

    /// <summary>
    /// Total value of the asset
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalValue { get; set; }

    /// <summary>
    /// Amount already paid
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal PaidAmount { get; set; }

    /// <summary>
    /// Pending amount (TotalValue - PaidAmount)
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal PendingAmount { get; set; }

    /// <summary>
    /// Purchase date
    /// </summary>
    public DateTime? PurchaseDate { get; set; }

    /// <summary>
    /// Additional notes
    /// </summary>
    [StringLength(MaxNotesLength)]
    public string Notes { get; set; }

    /// <summary>
    /// Last sync timestamp for conflict resolution
    /// </summary>
    public DateTime LastSyncedAt { get; set; }

    protected FtAsset()
    {
    }

    public FtAsset(
        Guid id,
        int tenantId,
        string clientId,
        string name,
        string assetType,
        decimal totalValue,
        decimal paidAmount)
    {
        Id = id;
        TenantId = tenantId;
        ClientId = clientId;
        Name = name;
        AssetType = assetType;
        TotalValue = totalValue;
        PaidAmount = paidAmount;
        PendingAmount = totalValue - paidAmount;
        LastSyncedAt = DateTime.UtcNow;
    }
}
