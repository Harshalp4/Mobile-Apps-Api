using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Bit2Sky.Bit2EHR.ChaiCount;

/// <summary>
/// Represents a raw material/supply item in inventory (Milk, Sugar, Tea Leaves, etc.)
/// </summary>
[Table("ChaiCountInventory")]
public class ChaiCountInventory : FullAuditedEntity<Guid>, IMustHaveTenant
{
    public const int MaxClientIdLength = 64;
    public const int MaxNameLength = 128;
    public const int MaxEmojiLength = 8;
    public const int MaxUnitLength = 32;

    public int TenantId { get; set; }

    /// <summary>
    /// Client-side unique identifier for sync
    /// </summary>
    [Required]
    [StringLength(MaxClientIdLength)]
    public string ClientId { get; set; }

    /// <summary>
    /// Name of the inventory item (Milk, Sugar, etc.)
    /// </summary>
    [Required]
    [StringLength(MaxNameLength)]
    public string Name { get; set; }

    /// <summary>
    /// Emoji icon
    /// </summary>
    [StringLength(MaxEmojiLength)]
    public string Emoji { get; set; }

    /// <summary>
    /// Current quantity in stock
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal Quantity { get; set; }

    /// <summary>
    /// Unit of measurement (L, kg, pieces, packets)
    /// </summary>
    [Required]
    [StringLength(MaxUnitLength)]
    public string Unit { get; set; }

    /// <summary>
    /// Threshold below which item is considered low stock
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal LowStockThreshold { get; set; }

    /// <summary>
    /// Cost per unit for expense tracking
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal CostPerUnit { get; set; }

    /// <summary>
    /// Last sync timestamp for conflict resolution
    /// </summary>
    public DateTime LastSyncedAt { get; set; }

    /// <summary>
    /// Check if item is below low stock threshold
    /// </summary>
    public bool IsLowStock => Quantity <= LowStockThreshold;

    protected ChaiCountInventory()
    {
    }

    public ChaiCountInventory(
        Guid id,
        int tenantId,
        string clientId,
        string name,
        string emoji,
        string unit,
        decimal quantity = 0,
        decimal lowStockThreshold = 1,
        decimal costPerUnit = 0)
    {
        Id = id;
        TenantId = tenantId;
        ClientId = clientId;
        Name = name;
        Emoji = emoji;
        Unit = unit;
        Quantity = quantity;
        LowStockThreshold = lowStockThreshold;
        CostPerUnit = costPerUnit;
        LastSyncedAt = DateTime.UtcNow;
    }

    public void UpdateQuantity(decimal newQuantity)
    {
        Quantity = newQuantity;
        LastSyncedAt = DateTime.UtcNow;
    }

    public void AddStock(decimal amount)
    {
        Quantity += amount;
        LastSyncedAt = DateTime.UtcNow;
    }

    public void UseStock(decimal amount)
    {
        Quantity = Math.Max(0, Quantity - amount);
        LastSyncedAt = DateTime.UtcNow;
    }
}
