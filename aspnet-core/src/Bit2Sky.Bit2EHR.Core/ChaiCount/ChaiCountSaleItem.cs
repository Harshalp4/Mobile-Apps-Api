using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Bit2Sky.Bit2EHR.ChaiCount;

/// <summary>
/// Represents an individual item in a sale
/// </summary>
[Table("ChaiCountSaleItems")]
public class ChaiCountSaleItem : CreationAuditedEntity<Guid>, IMustHaveTenant
{
    public const int MaxClientIdLength = 64;
    public const int MaxItemNameLength = 128;

    public int TenantId { get; set; }

    /// <summary>
    /// Client-side unique identifier for sync
    /// </summary>
    [Required]
    [StringLength(MaxClientIdLength)]
    public string ClientId { get; set; }

    /// <summary>
    /// Parent sale reference
    /// </summary>
    public Guid SaleId { get; set; }

    /// <summary>
    /// Parent sale
    /// </summary>
    [ForeignKey(nameof(SaleId))]
    public virtual ChaiCountSale Sale { get; set; }

    /// <summary>
    /// Item reference
    /// </summary>
    public Guid ItemId { get; set; }

    /// <summary>
    /// Item reference
    /// </summary>
    [ForeignKey(nameof(ItemId))]
    public virtual ChaiCountItem Item { get; set; }

    /// <summary>
    /// Item name at time of sale (denormalized for history)
    /// </summary>
    [Required]
    [StringLength(MaxItemNameLength)]
    public string ItemName { get; set; }

    /// <summary>
    /// Quantity sold
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Price per unit at time of sale
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Total amount for this line item
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalAmount { get; set; }

    protected ChaiCountSaleItem()
    {
    }

    public ChaiCountSaleItem(
        Guid id,
        int tenantId,
        string clientId,
        Guid saleId,
        Guid itemId,
        string itemName,
        int quantity,
        decimal unitPrice)
    {
        Id = id;
        TenantId = tenantId;
        ClientId = clientId;
        SaleId = saleId;
        ItemId = itemId;
        ItemName = itemName;
        Quantity = quantity;
        UnitPrice = unitPrice;
        TotalAmount = quantity * unitPrice;
    }
}
