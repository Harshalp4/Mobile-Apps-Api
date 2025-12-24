using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Bit2Sky.Bit2EHR.ChaiCount;

/// <summary>
/// Represents a stock purchase (expense) for tracking inventory costs
/// Used for profit calculation: Profit = Sales Revenue - Stock Purchase Costs
/// </summary>
[Table("ChaiCountStockPurchases")]
public class ChaiCountStockPurchase : FullAuditedEntity<Guid>, IMustHaveTenant
{
    public const int MaxClientIdLength = 64;
    public const int MaxNameLength = 128;
    public const int MaxUnitLength = 32;
    public const int MaxNoteLength = 256;

    public int TenantId { get; set; }

    /// <summary>
    /// Client-side unique identifier for sync
    /// </summary>
    [Required]
    [StringLength(MaxClientIdLength)]
    public string ClientId { get; set; }

    /// <summary>
    /// Reference to the inventory item purchased
    /// </summary>
    public Guid? InventoryItemId { get; set; }

    /// <summary>
    /// Name of the inventory item (denormalized for reports)
    /// </summary>
    [StringLength(MaxNameLength)]
    public string InventoryItemName { get; set; }

    /// <summary>
    /// Emoji of the inventory item
    /// </summary>
    [StringLength(8)]
    public string Emoji { get; set; }

    /// <summary>
    /// Quantity purchased (e.g., 500 for 500g)
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal Quantity { get; set; }

    /// <summary>
    /// Unit of measurement (g, kg, ml, L, pieces)
    /// </summary>
    [StringLength(MaxUnitLength)]
    public string Unit { get; set; }

    /// <summary>
    /// Cost per unit (e.g., ₹0.40 per gram or ₹400 per kg)
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal CostPerUnit { get; set; }

    /// <summary>
    /// Total cost paid for this purchase
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalCost { get; set; }

    /// <summary>
    /// Date when the stock was purchased
    /// </summary>
    public DateTime PurchaseDate { get; set; }

    /// <summary>
    /// Optional note about the purchase (supplier, etc.)
    /// </summary>
    [StringLength(MaxNoteLength)]
    public string Note { get; set; }

    /// <summary>
    /// Last sync timestamp for conflict resolution
    /// </summary>
    public DateTime LastSyncedAt { get; set; }

    protected ChaiCountStockPurchase()
    {
    }

    public ChaiCountStockPurchase(
        Guid id,
        int tenantId,
        string clientId,
        string inventoryItemName,
        decimal quantity,
        string unit,
        decimal costPerUnit,
        decimal totalCost,
        DateTime purchaseDate,
        Guid? inventoryItemId = null,
        string emoji = null,
        string note = null)
    {
        Id = id;
        TenantId = tenantId;
        ClientId = clientId;
        InventoryItemId = inventoryItemId;
        InventoryItemName = inventoryItemName;
        Emoji = emoji;
        Quantity = quantity;
        Unit = unit;
        CostPerUnit = costPerUnit;
        TotalCost = totalCost;
        PurchaseDate = purchaseDate;
        Note = note;
        LastSyncedAt = DateTime.UtcNow;
    }
}
