using System;
using Abp.Application.Services.Dto;

namespace Bit2Sky.Bit2EHR.FinTrack.Dto;

public class FtExpectedIncomeDto : EntityDto<Guid>
{
    public string ClientId { get; set; }
    public string Source { get; set; }
    public decimal Amount { get; set; }
    public string Frequency { get; set; }
    public DateTime NextDate { get; set; }
    public int? DayOfMonth { get; set; }
    public string Status { get; set; }
    public bool IsActive { get; set; }
    public string Notes { get; set; }
    public DateTime LastSyncedAt { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
}

public class CreateOrUpdateFtExpectedIncomeDto
{
    public string ClientId { get; set; }
    public string Source { get; set; }
    public decimal Amount { get; set; }
    public string Frequency { get; set; }
    public DateTime NextDate { get; set; }
    public int? DayOfMonth { get; set; }
    public string Status { get; set; }
    public bool IsActive { get; set; }
    public string Notes { get; set; }
    public DateTime LastSyncedAt { get; set; }
}
