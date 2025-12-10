using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Bit2Sky.Bit2EHR.FinTrack;

/// <summary>
/// Represents a bank account or wallet in FinTrack
/// </summary>
[Table("FtBankAccounts")]
public class FtBankAccount : FullAuditedEntity<Guid>, IMustHaveTenant
{
    public const int MaxClientIdLength = 64;
    public const int MaxNameLength = 128;
    public const int MaxTypeLength = 32;

    public int TenantId { get; set; }

    /// <summary>
    /// Client-side unique identifier for sync
    /// </summary>
    [Required]
    [StringLength(MaxClientIdLength)]
    public string ClientId { get; set; }

    /// <summary>
    /// Account name (e.g., "HDFC Savings", "Cash Wallet")
    /// </summary>
    [Required]
    [StringLength(MaxNameLength)]
    public string Name { get; set; }

    /// <summary>
    /// Account type: savings, current, wallet, credit
    /// </summary>
    [Required]
    [StringLength(MaxTypeLength)]
    public string AccountType { get; set; }

    /// <summary>
    /// Current balance
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal Balance { get; set; }

    /// <summary>
    /// Opening balance when account was created
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal OpeningBalance { get; set; }

    /// <summary>
    /// Whether this is the default account for transactions
    /// </summary>
    public bool IsDefault { get; set; }

    /// <summary>
    /// Display order for sorting
    /// </summary>
    public int DisplayOrder { get; set; }

    /// <summary>
    /// Last sync timestamp for conflict resolution
    /// </summary>
    public DateTime LastSyncedAt { get; set; }

    protected FtBankAccount()
    {
    }

    public FtBankAccount(
        Guid id,
        int tenantId,
        string clientId,
        string name,
        string accountType,
        decimal openingBalance)
    {
        Id = id;
        TenantId = tenantId;
        ClientId = clientId;
        Name = name;
        AccountType = accountType;
        OpeningBalance = openingBalance;
        Balance = openingBalance;
        IsDefault = false;
        DisplayOrder = 0;
        LastSyncedAt = DateTime.UtcNow;
    }
}
