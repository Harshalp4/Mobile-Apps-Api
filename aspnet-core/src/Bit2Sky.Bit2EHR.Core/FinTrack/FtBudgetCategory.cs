using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Bit2Sky.Bit2EHR.FinTrack;

/// <summary>
/// Represents a monthly budget allocation for a category in FinTrack
/// </summary>
[Table("FtBudgetCategories")]
public class FtBudgetCategory : FullAuditedEntity<Guid>, IMustHaveTenant
{
    public const int MaxClientIdLength = 64;
    public const int MaxNameLength = 64;

    public int TenantId { get; set; }

    /// <summary>
    /// Client-side unique identifier for sync
    /// </summary>
    [Required]
    [StringLength(MaxClientIdLength)]
    public string ClientId { get; set; }

    /// <summary>
    /// Category name
    /// </summary>
    [Required]
    [StringLength(MaxNameLength)]
    public string CategoryName { get; set; }

    /// <summary>
    /// Optional link to category entity
    /// </summary>
    public Guid? CategoryId { get; set; }

    /// <summary>
    /// Category reference
    /// </summary>
    [ForeignKey(nameof(CategoryId))]
    public virtual FtCategory Category { get; set; }

    /// <summary>
    /// Budget allocated amount
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal AllocatedAmount { get; set; }

    /// <summary>
    /// Amount spent so far
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal SpentAmount { get; set; }

    /// <summary>
    /// Month (1-12)
    /// </summary>
    public int Month { get; set; }

    /// <summary>
    /// Year
    /// </summary>
    public int Year { get; set; }

    /// <summary>
    /// Last sync timestamp for conflict resolution
    /// </summary>
    public DateTime LastSyncedAt { get; set; }

    protected FtBudgetCategory()
    {
    }

    public FtBudgetCategory(
        Guid id,
        int tenantId,
        string clientId,
        string categoryName,
        decimal allocatedAmount,
        int month,
        int year)
    {
        Id = id;
        TenantId = tenantId;
        ClientId = clientId;
        CategoryName = categoryName;
        AllocatedAmount = allocatedAmount;
        SpentAmount = 0;
        Month = month;
        Year = year;
        LastSyncedAt = DateTime.UtcNow;
    }
}
