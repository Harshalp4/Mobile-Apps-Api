using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Bit2Sky.Bit2EHR.ChaiCount;

/// <summary>
/// Promotional offers for ChaiCount
/// </summary>
[Table("ChaiCountOffers")]
public class ChaiCountOffer : FullAuditedEntity<Guid>, IMustHaveTenant
{
    public int TenantId { get; set; }

    /// <summary>
    /// Client-side unique identifier for sync
    /// </summary>
    [Required]
    [StringLength(100)]
    public string ClientId { get; set; }

    [Required]
    [StringLength(200)]
    public string Title { get; set; }

    [StringLength(500)]
    public string Description { get; set; }

    /// <summary>
    /// Promo code (optional)
    /// </summary>
    [StringLength(50)]
    public string Code { get; set; }

    /// <summary>
    /// Discount type: percentage, fixed_amount, free_item
    /// </summary>
    [StringLength(50)]
    public string DiscountType { get; set; }

    /// <summary>
    /// Discount value (percentage or amount)
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal DiscountValue { get; set; }

    /// <summary>
    /// Minimum order amount to apply offer
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal MinimumOrderAmount { get; set; }

    /// <summary>
    /// Start date of validity
    /// </summary>
    public DateTime? ValidFrom { get; set; }

    /// <summary>
    /// End date of validity
    /// </summary>
    public DateTime? ValidUntil { get; set; }

    /// <summary>
    /// Is offer currently active
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Number of times this offer has been used
    /// </summary>
    public int UsageCount { get; set; }

    /// <summary>
    /// Maximum usage limit (0 = unlimited)
    /// </summary>
    public int MaxUsageLimit { get; set; }

    /// <summary>
    /// Last synced timestamp
    /// </summary>
    public DateTime? LastSyncedAt { get; set; }
}
