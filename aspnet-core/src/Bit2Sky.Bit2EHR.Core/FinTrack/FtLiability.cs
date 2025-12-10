using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Bit2Sky.Bit2EHR.FinTrack;

/// <summary>
/// Represents a liability (loan, credit, debt) in FinTrack
/// </summary>
[Table("FtLiabilities")]
public class FtLiability : FullAuditedEntity<Guid>, IMustHaveTenant
{
    public const int MaxClientIdLength = 64;
    public const int MaxNameLength = 128;
    public const int MaxTypeLength = 32;
    public const int MaxNotesLength = 512;

    public int TenantId { get; set; }

    /// <summary>
    /// Client-side unique identifier for sync
    /// </summary>
    [Required]
    [StringLength(MaxClientIdLength)]
    public string ClientId { get; set; }

    /// <summary>
    /// Liability name (e.g., "Home Loan", "Car EMI")
    /// </summary>
    [Required]
    [StringLength(MaxNameLength)]
    public string Name { get; set; }

    /// <summary>
    /// Liability type: homeLoan, carLoan, personalLoan, creditCard, other
    /// </summary>
    [Required]
    [StringLength(MaxTypeLength)]
    public string LiabilityType { get; set; }

    /// <summary>
    /// Principal amount
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal Principal { get; set; }

    /// <summary>
    /// Amount already paid
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal PaidAmount { get; set; }

    /// <summary>
    /// Pending amount
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal PendingAmount { get; set; }

    /// <summary>
    /// Interest rate percentage
    /// </summary>
    [Column(TypeName = "decimal(5,2)")]
    public decimal? InterestRate { get; set; }

    /// <summary>
    /// Monthly EMI amount
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal? Emi { get; set; }

    /// <summary>
    /// EMI due day of month
    /// </summary>
    public int? EmiDueDay { get; set; }

    /// <summary>
    /// Start date of the loan
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// Expected end date
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Additional notes
    /// </summary>
    [StringLength(MaxNotesLength)]
    public string Notes { get; set; }

    /// <summary>
    /// Last sync timestamp for conflict resolution
    /// </summary>
    public DateTime LastSyncedAt { get; set; }

    protected FtLiability()
    {
    }

    public FtLiability(
        Guid id,
        int tenantId,
        string clientId,
        string name,
        string liabilityType,
        decimal principal,
        decimal paidAmount)
    {
        Id = id;
        TenantId = tenantId;
        ClientId = clientId;
        Name = name;
        LiabilityType = liabilityType;
        Principal = principal;
        PaidAmount = paidAmount;
        PendingAmount = principal - paidAmount;
        LastSyncedAt = DateTime.UtcNow;
    }
}
