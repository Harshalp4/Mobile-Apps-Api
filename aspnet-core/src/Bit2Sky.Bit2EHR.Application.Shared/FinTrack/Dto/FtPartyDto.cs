using System;
using Abp.Application.Services.Dto;

namespace Bit2Sky.Bit2EHR.FinTrack.Dto;

public class FtPartyDto : EntityDto<Guid>
{
    public string ClientId { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public decimal OwesYou { get; set; }
    public decimal YouOwe { get; set; }
    public decimal NetBalance { get; set; }
    public string Notes { get; set; }
    public DateTime LastSyncedAt { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
}

public class CreateOrUpdateFtPartyDto
{
    public string ClientId { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public decimal OwesYou { get; set; }
    public decimal YouOwe { get; set; }
    public decimal NetBalance { get; set; }
    public string Notes { get; set; }
    public DateTime LastSyncedAt { get; set; }
}
