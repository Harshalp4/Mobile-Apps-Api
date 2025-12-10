using System;
using Abp.Application.Services.Dto;

namespace Bit2Sky.Bit2EHR.FinTrack.Dto;

public class FtLiabilityDto : EntityDto<Guid>
{
    public string ClientId { get; set; }
    public string Name { get; set; }
    public string LiabilityType { get; set; }
    public decimal Principal { get; set; }
    public decimal PaidAmount { get; set; }
    public decimal PendingAmount { get; set; }
    public decimal? InterestRate { get; set; }
    public decimal? Emi { get; set; }
    public int? EmiDueDay { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Notes { get; set; }
    public DateTime LastSyncedAt { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
}

public class CreateOrUpdateFtLiabilityDto
{
    public string ClientId { get; set; }
    public string Name { get; set; }
    public string LiabilityType { get; set; }
    public decimal Principal { get; set; }
    public decimal PaidAmount { get; set; }
    public decimal PendingAmount { get; set; }
    public decimal? InterestRate { get; set; }
    public decimal? Emi { get; set; }
    public int? EmiDueDay { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Notes { get; set; }
    public DateTime LastSyncedAt { get; set; }
}
