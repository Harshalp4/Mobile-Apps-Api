using System;
using Abp.Application.Services.Dto;

namespace Bit2Sky.Bit2EHR.FinTrack.Dto;

public class FtTransactionDto : EntityDto<Guid>
{
    public string ClientId { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public string TransactionType { get; set; }
    public string PaymentMode { get; set; }
    public DateTime TransactionDate { get; set; }
    public string AccountClientId { get; set; }
    public string CategoryClientId { get; set; }
    public string PartyClientId { get; set; }
    public string Notes { get; set; }
    public DateTime LastSyncedAt { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
}

public class CreateOrUpdateFtTransactionDto
{
    public string ClientId { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public string TransactionType { get; set; }
    public string PaymentMode { get; set; }
    public DateTime TransactionDate { get; set; }
    public string AccountClientId { get; set; }
    public string CategoryClientId { get; set; }
    public string PartyClientId { get; set; }
    public string Notes { get; set; }
    public DateTime LastSyncedAt { get; set; }
}
