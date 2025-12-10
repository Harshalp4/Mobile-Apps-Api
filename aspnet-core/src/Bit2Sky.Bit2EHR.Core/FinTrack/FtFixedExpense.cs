using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Bit2Sky.Bit2EHR.FinTrack;

/// <summary>
/// Represents a monthly fixed expense in FinTrack
/// </summary>
[Table("FtFixedExpenses")]
public class FtFixedExpense : FullAuditedEntity<Guid>, IMustHaveTenant
{
    public const int MaxClientIdLength = 64;
    public const int MaxNameLength = 128;
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
    /// Expense name (e.g., "Rent", "Internet", "Insurance")
    /// </summary>
    [Required]
    [StringLength(MaxNameLength)]
    public string Name { get; set; }

    /// <summary>
    /// Fixed amount
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Due day of month (1-31)
    /// </summary>
    public int DueDate { get; set; }

    /// <summary>
    /// Month (1-12)
    /// </summary>
    public int Month { get; set; }

    /// <summary>
    /// Year
    /// </summary>
    public int Year { get; set; }

    /// <summary>
    /// Status: pending, paid
    /// </summary>
    [Required]
    [StringLength(MaxStatusLength)]
    public string Status { get; set; }

    /// <summary>
    /// Date when marked as paid
    /// </summary>
    public DateTime? PaidDate { get; set; }

    /// <summary>
    /// Whether this is a recurring expense
    /// </summary>
    public bool IsRecurring { get; set; }

    /// <summary>
    /// Additional notes
    /// </summary>
    [StringLength(MaxNotesLength)]
    public string Notes { get; set; }

    /// <summary>
    /// Last sync timestamp for conflict resolution
    /// </summary>
    public DateTime LastSyncedAt { get; set; }

    protected FtFixedExpense()
    {
    }

    public FtFixedExpense(
        Guid id,
        int tenantId,
        string clientId,
        string name,
        decimal amount,
        int dueDate,
        int month,
        int year)
    {
        Id = id;
        TenantId = tenantId;
        ClientId = clientId;
        Name = name;
        Amount = amount;
        DueDate = dueDate;
        Month = month;
        Year = year;
        Status = "pending";
        IsRecurring = true;
        LastSyncedAt = DateTime.UtcNow;
    }
}
