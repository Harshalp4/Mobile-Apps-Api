using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Bit2Sky.Bit2EHR.ChaiCount;

/// <summary>
/// Represents a menu item in ChaiCount (Tea, Coffee, Snacks, etc.)
/// </summary>
[Table("ChaiCountItems")]
public class ChaiCountItem : FullAuditedEntity<Guid>, IMustHaveTenant
{
    public const int MaxNameLength = 128;
    public const int MaxEmojiLength = 8;
    public const int MaxCategoryLength = 64;
    public const int MaxClientIdLength = 64;

    public int TenantId { get; set; }

    /// <summary>
    /// Client-side unique identifier for sync
    /// </summary>
    [Required]
    [StringLength(MaxClientIdLength)]
    public string ClientId { get; set; }

    /// <summary>
    /// Display name of the item
    /// </summary>
    [Required]
    [StringLength(MaxNameLength)]
    public string Name { get; set; }

    /// <summary>
    /// Emoji icon for the item
    /// </summary>
    [StringLength(MaxEmojiLength)]
    public string Emoji { get; set; }

    /// <summary>
    /// Category of the item (Tea, Coffee, Snacks, etc.)
    /// </summary>
    [StringLength(MaxCategoryLength)]
    public string Category { get; set; }

    /// <summary>
    /// Price per unit
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    /// <summary>
    /// Current stock quantity (for inventory tracking)
    /// </summary>
    public int StockQuantity { get; set; }

    /// <summary>
    /// Minimum stock level for alerts
    /// </summary>
    public int MinStockLevel { get; set; }

    /// <summary>
    /// Display order for sorting
    /// </summary>
    public int DisplayOrder { get; set; }

    /// <summary>
    /// Whether the item is active and available for sale
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Last sync timestamp for conflict resolution
    /// </summary>
    public DateTime LastSyncedAt { get; set; }

    protected ChaiCountItem()
    {
    }

    public ChaiCountItem(
        Guid id,
        int tenantId,
        string clientId,
        string name,
        string emoji,
        decimal price,
        string category = null)
    {
        Id = id;
        TenantId = tenantId;
        ClientId = clientId;
        Name = name;
        Emoji = emoji;
        Price = price;
        Category = category;
        IsActive = true;
        LastSyncedAt = DateTime.UtcNow;
    }
}
