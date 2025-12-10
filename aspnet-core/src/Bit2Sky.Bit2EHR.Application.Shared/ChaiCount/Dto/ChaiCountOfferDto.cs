using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;

namespace Bit2Sky.Bit2EHR.ChaiCount.Dto;

/// <summary>
/// DTO for offers
/// </summary>
public class ChaiCountOfferDto : EntityDto<Guid>
{
    public string ClientId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Code { get; set; }
    public string DiscountType { get; set; }
    public decimal DiscountValue { get; set; }
    public decimal MinimumOrderAmount { get; set; }
    public DateTime? ValidFrom { get; set; }
    public DateTime? ValidUntil { get; set; }
    public bool IsActive { get; set; }
    public int UsageCount { get; set; }
    public int MaxUsageLimit { get; set; }
    public DateTime? LastSyncedAt { get; set; }
    public bool IsValid { get; set; }
}

/// <summary>
/// Input DTO for creating/updating offers
/// </summary>
public class CreateOrUpdateOfferDto
{
    public string ClientId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Code { get; set; }
    public string DiscountType { get; set; }
    public decimal DiscountValue { get; set; }
    public decimal MinimumOrderAmount { get; set; }
    public DateTime? ValidFrom { get; set; }
    public DateTime? ValidUntil { get; set; }
    public bool IsActive { get; set; }
    public int MaxUsageLimit { get; set; }
}

/// <summary>
/// Input DTO for syncing offers
/// </summary>
public class SyncOffersInput
{
    public List<CreateOrUpdateOfferDto> Offers { get; set; }
}
