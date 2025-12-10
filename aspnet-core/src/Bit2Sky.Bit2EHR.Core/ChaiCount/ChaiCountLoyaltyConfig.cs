using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Bit2Sky.Bit2EHR.ChaiCount;

/// <summary>
/// Loyalty program configuration for ChaiCount
/// </summary>
[Table("ChaiCountLoyaltyConfig")]
public class ChaiCountLoyaltyConfig : FullAuditedEntity<Guid>, IMustHaveTenant
{
    public int TenantId { get; set; }

    /// <summary>
    /// Client-side unique identifier for sync
    /// </summary>
    [StringLength(100)]
    public string ClientId { get; set; }

    /// <summary>
    /// Is loyalty program enabled
    /// </summary>
    public bool IsEnabled { get; set; } = true;

    /// <summary>
    /// Points earned per item purchased
    /// </summary>
    public int PointsPerItem { get; set; } = 1;

    /// <summary>
    /// Points earned per rupee spent
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal PointsPerRupee { get; set; }

    /// <summary>
    /// Points needed to get reward
    /// </summary>
    public int RewardThreshold { get; set; } = 10;

    /// <summary>
    /// Reward type: free_item, discount, cashback
    /// </summary>
    [StringLength(50)]
    public string RewardType { get; set; } = "free_item";

    /// <summary>
    /// Reward value (1 for free item, or discount amount)
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal RewardValue { get; set; } = 1;

    /// <summary>
    /// Description of the reward
    /// </summary>
    [StringLength(200)]
    public string RewardDescription { get; set; }

    /// <summary>
    /// Points expiry days (0 = never expire)
    /// </summary>
    public int PointsExpiryDays { get; set; }

    /// <summary>
    /// Last synced timestamp
    /// </summary>
    public DateTime? LastSyncedAt { get; set; }
}
