using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;

namespace Bit2Sky.Bit2EHR.ChaiCount.Dto;

public class ChaiCountCreditTransactionDto : EntityDto<Guid>
{
    public string ClientId { get; set; }
    public Guid CustomerId { get; set; }
    public string CustomerClientId { get; set; }
    public string CustomerName { get; set; }
    public DateTime TransactionDate { get; set; }
    public string TransactionType { get; set; }
    public decimal Amount { get; set; }
    public decimal BalanceAfter { get; set; }
    public string ItemsSummary { get; set; }
    public string Notes { get; set; }
    public DateTime LastSyncedAt { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
}

public class CreateOrUpdateChaiCountCreditTransactionDto
{
    public string ClientId { get; set; }
    public string CustomerClientId { get; set; }
    public DateTime TransactionDate { get; set; }
    public string TransactionType { get; set; }
    public decimal Amount { get; set; }
    public decimal BalanceAfter { get; set; }
    public string ItemsSummary { get; set; }
    public string Notes { get; set; }
    public DateTime LastSyncedAt { get; set; }
}

public class UdhariSummaryDto
{
    public decimal TotalOutstanding { get; set; }
    public int TotalUdhariCustomers { get; set; }
    public decimal TotalCreditGiven { get; set; }
    public decimal TotalPaymentsReceived { get; set; }
    public List<UdhariCustomerSummaryDto> TopDebtors { get; set; }
}

public class UdhariCustomerSummaryDto
{
    public Guid CustomerId { get; set; }
    public string CustomerClientId { get; set; }
    public string CustomerName { get; set; }
    public string Organization { get; set; }
    public string Phone { get; set; }
    public decimal CreditBalance { get; set; }
    public DateTime? LastTransactionDate { get; set; }
}
