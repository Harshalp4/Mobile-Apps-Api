using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Bit2Sky.Bit2EHR.FinTrack;

/// <summary>
/// Represents a financial transaction (income or expense) in FinTrack
/// </summary>
[Table("FtTransactions")]
public class FtTransaction : FullAuditedEntity<Guid>, IMustHaveTenant
{
    public const int MaxClientIdLength = 64;
    public const int MaxDescriptionLength = 256;
    public const int MaxTypeLength = 16;
    public const int MaxPaymentModeLength = 32;
    public const int MaxNotesLength = 512;

    public int TenantId { get; set; }

    /// <summary>
    /// Client-side unique identifier for sync
    /// </summary>
    [Required]
    [StringLength(MaxClientIdLength)]
    public string ClientId { get; set; }

    /// <summary>
    /// Transaction description
    /// </summary>
    [Required]
    [StringLength(MaxDescriptionLength)]
    public string Description { get; set; }

    /// <summary>
    /// Transaction amount
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Transaction type: moneyIn or moneyOut
    /// </summary>
    [Required]
    [StringLength(MaxTypeLength)]
    public string TransactionType { get; set; }

    /// <summary>
    /// Payment mode: cash, upi, card, netBanking, etc.
    /// </summary>
    [Required]
    [StringLength(MaxPaymentModeLength)]
    public string PaymentMode { get; set; }

    /// <summary>
    /// Date of transaction
    /// </summary>
    public DateTime TransactionDate { get; set; }

    /// <summary>
    /// Associated bank account ID
    /// </summary>
    public Guid AccountId { get; set; }

    /// <summary>
    /// Bank account reference
    /// </summary>
    [ForeignKey(nameof(AccountId))]
    public virtual FtBankAccount Account { get; set; }

    /// <summary>
    /// Optional category ID
    /// </summary>
    public Guid? CategoryId { get; set; }

    /// <summary>
    /// Category reference
    /// </summary>
    [ForeignKey(nameof(CategoryId))]
    public virtual FtCategory Category { get; set; }

    /// <summary>
    /// Optional party ID (for people money tracker)
    /// </summary>
    public Guid? PartyId { get; set; }

    /// <summary>
    /// Party reference
    /// </summary>
    [ForeignKey(nameof(PartyId))]
    public virtual FtParty Party { get; set; }

    /// <summary>
    /// Additional notes
    /// </summary>
    [StringLength(MaxNotesLength)]
    public string Notes { get; set; }

    /// <summary>
    /// Last sync timestamp for conflict resolution
    /// </summary>
    public DateTime LastSyncedAt { get; set; }

    protected FtTransaction()
    {
    }

    public FtTransaction(
        Guid id,
        int tenantId,
        string clientId,
        string description,
        decimal amount,
        string transactionType,
        string paymentMode,
        DateTime transactionDate,
        Guid accountId)
    {
        Id = id;
        TenantId = tenantId;
        ClientId = clientId;
        Description = description;
        Amount = amount;
        TransactionType = transactionType;
        PaymentMode = paymentMode;
        TransactionDate = transactionDate;
        AccountId = accountId;
        LastSyncedAt = DateTime.UtcNow;
    }
}
