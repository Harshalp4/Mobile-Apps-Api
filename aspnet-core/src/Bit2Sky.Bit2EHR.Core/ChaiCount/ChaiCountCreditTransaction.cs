using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Bit2Sky.Bit2EHR.ChaiCount;

/// <summary>
/// Represents a credit transaction (Udhari) in ChaiCount
/// Can be a credit sale or a payment
/// </summary>
[Table("ChaiCountCreditTransactions")]
public class ChaiCountCreditTransaction : FullAuditedEntity<Guid>, IMustHaveTenant
{
    public const int MaxClientIdLength = 64;
    public const int MaxNotesLength = 512;
    public const int MaxItemsSummaryLength = 1024;
    public const int MaxTransactionTypeLength = 20;

    public int TenantId { get; set; }

    /// <summary>
    /// Client-side unique identifier for sync
    /// </summary>
    [Required]
    [StringLength(MaxClientIdLength)]
    public string ClientId { get; set; }

    /// <summary>
    /// Reference to the customer
    /// </summary>
    [Required]
    public Guid CustomerId { get; set; }

    /// <summary>
    /// Customer reference
    /// </summary>
    [ForeignKey(nameof(CustomerId))]
    public virtual ChaiCountCustomer Customer { get; set; }

    /// <summary>
    /// Transaction date
    /// </summary>
    public DateTime TransactionDate { get; set; }

    /// <summary>
    /// Type of transaction: 'credit' (sale on credit) or 'payment' (payment received)
    /// </summary>
    [Required]
    [StringLength(MaxTransactionTypeLength)]
    public string TransactionType { get; set; }

    /// <summary>
    /// Transaction amount
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Running balance after this transaction
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal BalanceAfter { get; set; }

    /// <summary>
    /// Summary of items sold (for credit transactions)
    /// </summary>
    [StringLength(MaxItemsSummaryLength)]
    public string ItemsSummary { get; set; }

    /// <summary>
    /// Additional notes
    /// </summary>
    [StringLength(MaxNotesLength)]
    public string Notes { get; set; }

    /// <summary>
    /// Last sync timestamp for conflict resolution
    /// </summary>
    public DateTime LastSyncedAt { get; set; }

    protected ChaiCountCreditTransaction()
    {
    }

    public ChaiCountCreditTransaction(
        Guid id,
        int tenantId,
        string clientId,
        Guid customerId,
        DateTime transactionDate,
        string transactionType,
        decimal amount,
        decimal balanceAfter)
    {
        Id = id;
        TenantId = tenantId;
        ClientId = clientId;
        CustomerId = customerId;
        TransactionDate = transactionDate;
        TransactionType = transactionType;
        Amount = amount;
        BalanceAfter = balanceAfter;
        LastSyncedAt = DateTime.UtcNow;
    }
}
