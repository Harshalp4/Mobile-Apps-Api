using System;
using Abp.Application.Services.Dto;

namespace Bit2Sky.Bit2EHR.ChaiCount.Dto;

/// <summary>
/// DTO for loyalty configuration
/// </summary>
public class ChaiCountLoyaltyConfigDto : EntityDto<Guid>
{
    public string ClientId { get; set; }
    public bool IsEnabled { get; set; }
    public int PointsPerItem { get; set; }
    public decimal PointsPerRupee { get; set; }
    public int RewardThreshold { get; set; }
    public string RewardType { get; set; }
    public decimal RewardValue { get; set; }
    public string RewardDescription { get; set; }
    public int PointsExpiryDays { get; set; }
    public DateTime? LastSyncedAt { get; set; }
}

/// <summary>
/// Input DTO for syncing loyalty config
/// </summary>
public class SyncLoyaltyConfigInput
{
    public string ClientId { get; set; }
    public bool IsEnabled { get; set; }
    public int PointsPerItem { get; set; }
    public decimal PointsPerRupee { get; set; }
    public int RewardThreshold { get; set; }
    public string RewardType { get; set; }
    public decimal RewardValue { get; set; }
    public string RewardDescription { get; set; }
    public int PointsExpiryDays { get; set; }
}
