using System;
using Abp.Application.Services.Dto;

namespace Bit2Sky.Bit2EHR.FinTrack.Dto;

public class FtFixedExpenseDto : EntityDto<Guid>
{
    public string ClientId { get; set; }
    public string Name { get; set; }
    public decimal Amount { get; set; }
    public int DueDate { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    public string Status { get; set; }
    public DateTime? PaidDate { get; set; }
    public bool IsRecurring { get; set; }
    public string Notes { get; set; }
    public DateTime LastSyncedAt { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
}

public class CreateOrUpdateFtFixedExpenseDto
{
    public string ClientId { get; set; }
    public string Name { get; set; }
    public decimal Amount { get; set; }
    public int DueDate { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    public string Status { get; set; }
    public DateTime? PaidDate { get; set; }
    public bool IsRecurring { get; set; }
    public string Notes { get; set; }
    public DateTime LastSyncedAt { get; set; }
}
