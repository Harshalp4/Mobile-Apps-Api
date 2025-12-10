using System;
using Abp.Application.Services.Dto;

namespace Bit2Sky.Bit2EHR.ChaiCount.Dto;

public class ChaiCountItemDto : EntityDto<Guid>
{
    public string ClientId { get; set; }
    public string Name { get; set; }
    public string Emoji { get; set; }
    public string Category { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public int MinStockLevel { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; }
    public DateTime LastSyncedAt { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
}

public class CreateOrUpdateChaiCountItemDto
{
    public string ClientId { get; set; }
    public string Name { get; set; }
    public string Emoji { get; set; }
    public string Category { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public int MinStockLevel { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; }
    public DateTime LastSyncedAt { get; set; }
}
