using System;
using Abp.Application.Services.Dto;

namespace Bit2Sky.Bit2EHR.FinTrack.Dto;

public class FtBudgetCategoryDto : EntityDto<Guid>
{
    public string ClientId { get; set; }
    public string CategoryName { get; set; }
    public string CategoryClientId { get; set; }
    public decimal AllocatedAmount { get; set; }
    public decimal SpentAmount { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    public DateTime LastSyncedAt { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
}

public class CreateOrUpdateFtBudgetCategoryDto
{
    public string ClientId { get; set; }
    public string CategoryName { get; set; }
    public string CategoryClientId { get; set; }
    public decimal AllocatedAmount { get; set; }
    public decimal SpentAmount { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    public DateTime LastSyncedAt { get; set; }
}
