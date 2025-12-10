using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Bit2Sky.Bit2EHR.ChaiCount;

/// <summary>
/// Maps menu items to inventory items with usage quantity
/// </summary>
[Table("ChaiCountItemUsage")]
public class ChaiCountItemUsage : FullAuditedEntity<Guid>, IMustHaveTenant
{
    public int TenantId { get; set; }

    /// <summary>
    /// Client-side unique identifier for sync
    /// </summary>
    [Required]
    [StringLength(100)]
    public string ClientId { get; set; }

    /// <summary>
    /// Menu item client ID
    /// </summary>
    [Required]
    [StringLength(100)]
    public string MenuItemClientId { get; set; }

    /// <summary>
    /// Inventory item client ID
    /// </summary>
    [Required]
    [StringLength(100)]
    public string InventoryItemClientId { get; set; }

    /// <summary>
    /// Quantity of inventory item used per menu item sale
    /// </summary>
    [Column(TypeName = "decimal(18,4)")]
    public decimal QuantityPerSale { get; set; }

    /// <summary>
    /// Last synced timestamp
    /// </summary>
    public DateTime? LastSyncedAt { get; set; }
}
