using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Bit2Sky.Bit2EHR.ChaiCount;

/// <summary>
/// Represents a daily sales record in ChaiCount
/// </summary>
[Table("ChaiCountSales")]
public class ChaiCountSale : FullAuditedEntity<Guid>, IMustHaveTenant
{
    public const int MaxClientIdLength = 64;
    public const int MaxNotesLength = 512;

    public int TenantId { get; set; }

    /// <summary>
    /// Client-side unique identifier for sync
    /// </summary>
    [Required]
    [StringLength(MaxClientIdLength)]
    public string ClientId { get; set; }

    /// <summary>
    /// Date of the sale (without time component)
    /// </summary>
    public DateTime SaleDate { get; set; }

    /// <summary>
    /// Total revenue for this sale
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Total number of items sold
    /// </summary>
    public int TotalItems { get; set; }

    /// <summary>
    /// Optional customer reference
    /// </summary>
    public Guid? CustomerId { get; set; }

    /// <summary>
    /// Customer reference
    /// </summary>
    [ForeignKey(nameof(CustomerId))]
    public virtual ChaiCountCustomer Customer { get; set; }

    /// <summary>
    /// Additional notes about the sale
    /// </summary>
    [StringLength(MaxNotesLength)]
    public string Notes { get; set; }

    /// <summary>
    /// Whether the day has been closed
    /// </summary>
    public bool IsDayClosed { get; set; }

    /// <summary>
    /// Last sync timestamp for conflict resolution
    /// </summary>
    public DateTime LastSyncedAt { get; set; }

    /// <summary>
    /// Individual item sales in this sale
    /// </summary>
    public virtual ICollection<ChaiCountSaleItem> SaleItems { get; set; }

    protected ChaiCountSale()
    {
        SaleItems = new List<ChaiCountSaleItem>();
    }

    public ChaiCountSale(
        Guid id,
        int tenantId,
        string clientId,
        DateTime saleDate,
        decimal totalAmount,
        int totalItems)
    {
        Id = id;
        TenantId = tenantId;
        ClientId = clientId;
        SaleDate = saleDate.Date;
        TotalAmount = totalAmount;
        TotalItems = totalItems;
        IsDayClosed = false;
        LastSyncedAt = DateTime.UtcNow;
        SaleItems = new List<ChaiCountSaleItem>();
    }
}
