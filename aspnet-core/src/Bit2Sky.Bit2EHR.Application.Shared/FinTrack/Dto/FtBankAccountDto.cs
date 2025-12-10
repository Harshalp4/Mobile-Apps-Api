using System;
using Abp.Application.Services.Dto;

namespace Bit2Sky.Bit2EHR.FinTrack.Dto;

public class FtBankAccountDto : EntityDto<Guid>
{
    public string ClientId { get; set; }
    public string Name { get; set; }
    public string AccountType { get; set; }
    public decimal Balance { get; set; }
    public decimal OpeningBalance { get; set; }
    public bool IsDefault { get; set; }
    public int DisplayOrder { get; set; }
    public DateTime LastSyncedAt { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
}

public class CreateOrUpdateFtBankAccountDto
{
    public string ClientId { get; set; }
    public string Name { get; set; }
    public string AccountType { get; set; }
    public decimal Balance { get; set; }
    public decimal OpeningBalance { get; set; }
    public bool IsDefault { get; set; }
    public int DisplayOrder { get; set; }
    public DateTime LastSyncedAt { get; set; }
}
