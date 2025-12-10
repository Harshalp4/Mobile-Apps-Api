using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Bit2Sky.Bit2EHR.FinTrack;

/// <summary>
/// Represents an expected recurring income in FinTrack
/// </summary>
[Table("FtExpectedIncomes")]
public class FtExpectedIncome : FullAuditedEntity<Guid>, IMustHaveTenant
{
    public const int MaxClientIdLength = 64;
    public const int MaxSourceLength = 128;
    public const int MaxFrequencyLength = 16;
    public const int MaxStatusLength = 16;
    public const int MaxNotesLength = 512;

    public int TenantId { get; set; }

    /// <summary>
    /// Client-side unique identifier for sync
    /// </summary>
    [Required]
    [StringLength(MaxClientIdLength)]
    public string ClientId { get; set; }

    /// <summary>
    /// Income source (e.g., "Salary", "Freelance", "Rent")
    /// </summary>
    [Required]
    [StringLength(MaxSourceLength)]
    public string Source { get; set; }

    /// <summary>
    /// Expected amount
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Frequency: monthly, weekly, yearly, oneTime
    /// </summary>
    [Required]
    [StringLength(MaxFrequencyLength)]
    public string Frequency { get; set; }

    /// <summary>
    /// Next expected date
    /// </summary>
    public DateTime NextDate { get; set; }

    /// <summary>
    /// Day of month for recurring (1-31)
    /// </summary>
    public int? DayOfMonth { get; set; }

    /// <summary>
    /// Status: pending, received, overdue
    /// </summary>
    [Required]
    [StringLength(MaxStatusLength)]
    public string Status { get; set; }

    /// <summary>
    /// Whether this income is active
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Additional notes
    /// </summary>
    [StringLength(MaxNotesLength)]
    public string Notes { get; set; }

    /// <summary>
    /// Last sync timestamp for conflict resolution
    /// </summary>
    public DateTime LastSyncedAt { get; set; }

    protected FtExpectedIncome()
    {
    }

    public FtExpectedIncome(
        Guid id,
        int tenantId,
        string clientId,
        string source,
        decimal amount,
        string frequency,
        DateTime nextDate)
    {
        Id = id;
        TenantId = tenantId;
        ClientId = clientId;
        Source = source;
        Amount = amount;
        Frequency = frequency;
        NextDate = nextDate;
        Status = "pending";
        IsActive = true;
        LastSyncedAt = DateTime.UtcNow;
    }
}
