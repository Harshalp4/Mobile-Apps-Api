using System;
using Abp.Application.Services.Dto;

namespace Bit2Sky.Bit2EHR.FinTrack.Dto;

public class FtAssetDto : EntityDto<Guid>
{
    public string ClientId { get; set; }
    public string Name { get; set; }
    public string AssetType { get; set; }
    public decimal TotalValue { get; set; }
    public decimal PaidAmount { get; set; }
    public decimal PendingAmount { get; set; }
    public DateTime? PurchaseDate { get; set; }
    public string Notes { get; set; }
    public DateTime LastSyncedAt { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
}

public class CreateOrUpdateFtAssetDto
{
    public string ClientId { get; set; }
    public string Name { get; set; }
    public string AssetType { get; set; }
    public decimal TotalValue { get; set; }
    public decimal PaidAmount { get; set; }
    public decimal PendingAmount { get; set; }
    public DateTime? PurchaseDate { get; set; }
    public string Notes { get; set; }
    public DateTime LastSyncedAt { get; set; }
}
