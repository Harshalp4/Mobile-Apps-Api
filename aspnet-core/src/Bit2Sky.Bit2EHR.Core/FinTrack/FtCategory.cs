using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Bit2Sky.Bit2EHR.FinTrack;

/// <summary>
/// Represents a transaction category in FinTrack
/// </summary>
[Table("FtCategories")]
public class FtCategory : FullAuditedEntity<Guid>, IMustHaveTenant
{
    public const int MaxClientIdLength = 64;
    public const int MaxNameLength = 64;
    public const int MaxIconLength = 16;
    public const int MaxColorLength = 16;
    public const int MaxTypeLength = 16;

    public int TenantId { get; set; }

    /// <summary>
    /// Client-side unique identifier for sync
    /// </summary>
    [Required]
    [StringLength(MaxClientIdLength)]
    public string ClientId { get; set; }

    /// <summary>
    /// Category name (e.g., "Food", "Transport", "Salary")
    /// </summary>
    [Required]
    [StringLength(MaxNameLength)]
    public string Name { get; set; }

    /// <summary>
    /// Icon identifier or emoji
    /// </summary>
    [StringLength(MaxIconLength)]
    public string Icon { get; set; }

    /// <summary>
    /// Color hex code for display
    /// </summary>
    [StringLength(MaxColorLength)]
    public string Color { get; set; }

    /// <summary>
    /// Category type: income, expense, or both
    /// </summary>
    [Required]
    [StringLength(MaxTypeLength)]
    public string CategoryType { get; set; }

    /// <summary>
    /// Whether this is a system default category
    /// </summary>
    public bool IsSystem { get; set; }

    /// <summary>
    /// Display order for sorting
    /// </summary>
    public int DisplayOrder { get; set; }

    /// <summary>
    /// Last sync timestamp for conflict resolution
    /// </summary>
    public DateTime LastSyncedAt { get; set; }

    protected FtCategory()
    {
    }

    public FtCategory(
        Guid id,
        int tenantId,
        string clientId,
        string name,
        string categoryType,
        string icon = null,
        string color = null)
    {
        Id = id;
        TenantId = tenantId;
        ClientId = clientId;
        Name = name;
        CategoryType = categoryType;
        Icon = icon;
        Color = color;
        IsSystem = false;
        DisplayOrder = 0;
        LastSyncedAt = DateTime.UtcNow;
    }
}
