using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;

namespace Bit2Sky.Bit2EHR.ChaiCount.Dto;

/// <summary>
/// DTO for item usage mapping
/// </summary>
public class ChaiCountItemUsageDto : EntityDto<Guid>
{
    public string ClientId { get; set; }
    public string MenuItemClientId { get; set; }
    public string InventoryItemClientId { get; set; }
    public decimal QuantityPerSale { get; set; }
    public DateTime? LastSyncedAt { get; set; }
}

/// <summary>
/// Input DTO for creating/updating item usage
/// </summary>
public class CreateOrUpdateItemUsageDto
{
    public string ClientId { get; set; }
    public string MenuItemClientId { get; set; }
    public string InventoryItemClientId { get; set; }
    public decimal QuantityPerSale { get; set; }
}

/// <summary>
/// Input DTO for syncing item usage mappings
/// </summary>
public class SyncItemUsageInput
{
    public List<CreateOrUpdateItemUsageDto> Mappings { get; set; }
}
