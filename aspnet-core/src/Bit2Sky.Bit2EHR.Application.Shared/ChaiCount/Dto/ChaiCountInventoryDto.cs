using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;

namespace Bit2Sky.Bit2EHR.ChaiCount.Dto;

public class ChaiCountInventoryDto : EntityDto<Guid>
{
    public string ClientId { get; set; }
    public string Name { get; set; }
    public string Emoji { get; set; }
    public decimal Quantity { get; set; }
    public string Unit { get; set; }
    public decimal LowStockThreshold { get; set; }
    public decimal CostPerUnit { get; set; }
    public bool IsLowStock { get; set; }
    public DateTime LastSyncedAt { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
}

public class CreateOrUpdateInventoryDto
{
    public string ClientId { get; set; }
    public string Name { get; set; }
    public string Emoji { get; set; }
    public decimal Quantity { get; set; }
    public string Unit { get; set; }
    public decimal LowStockThreshold { get; set; }
    public decimal CostPerUnit { get; set; }
    public DateTime LastSyncedAt { get; set; }
}

/// <summary>
/// Request DTO for syncing inventory from mobile to server
/// </summary>
public class SyncInventoryInput
{
    public List<CreateOrUpdateInventoryDto> Items { get; set; }
}

/// <summary>
/// Response DTO for low stock alerts
/// </summary>
public class LowStockAlertDto
{
    public List<ChaiCountInventoryDto> LowStockItems { get; set; }
    public int TotalLowStockCount { get; set; }
}
