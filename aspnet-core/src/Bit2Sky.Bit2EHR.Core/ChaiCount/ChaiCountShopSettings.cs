using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Bit2Sky.Bit2EHR.ChaiCount;

/// <summary>
/// Shop settings for ChaiCount
/// </summary>
[Table("ChaiCountShopSettings")]
public class ChaiCountShopSettings : FullAuditedEntity<Guid>, IMustHaveTenant
{
    public int TenantId { get; set; }

    /// <summary>
    /// Client-side unique identifier for sync
    /// </summary>
    [StringLength(100)]
    public string ClientId { get; set; }

    [Required]
    [StringLength(200)]
    public string ShopName { get; set; }

    [StringLength(200)]
    public string OwnerName { get; set; }

    [StringLength(20)]
    public string PhoneNumber { get; set; }

    [StringLength(500)]
    public string Address { get; set; }

    [StringLength(100)]
    public string UpiId { get; set; }

    [StringLength(500)]
    public string GoogleReviewLink { get; set; }

    [StringLength(200)]
    public string ReportEmail { get; set; }

    /// <summary>
    /// Currency symbol (default: ₹)
    /// </summary>
    [StringLength(10)]
    public string CurrencySymbol { get; set; } = "₹";

    /// <summary>
    /// Timezone for reports
    /// </summary>
    [StringLength(50)]
    public string Timezone { get; set; } = "Asia/Kolkata";

    /// <summary>
    /// Last synced timestamp
    /// </summary>
    public DateTime? LastSyncedAt { get; set; }

    // ============ Feature Toggles ============

    /// <summary>
    /// Enable AI-powered features (predictions, insights)
    /// </summary>
    public bool AiEnabled { get; set; } = false;

    /// <summary>
    /// Enable voice command features
    /// </summary>
    public bool VoiceCommandsEnabled { get; set; } = false;

    /// <summary>
    /// Enable WhatsApp integration
    /// </summary>
    public bool WhatsAppEnabled { get; set; } = false;

    /// <summary>
    /// WhatsApp Business phone number
    /// </summary>
    [StringLength(20)]
    public string WhatsAppNumber { get; set; }

    /// <summary>
    /// Enable daily report auto-send
    /// </summary>
    public bool DailyReportEnabled { get; set; } = false;

    /// <summary>
    /// Time to send daily report (HH:mm format)
    /// </summary>
    [StringLength(10)]
    public string DailyReportTime { get; set; } = "20:00";

    /// <summary>
    /// Enable low stock alerts
    /// </summary>
    public bool LowStockAlertsEnabled { get; set; } = true;

    /// <summary>
    /// Enable sales predictions
    /// </summary>
    public bool SalesPredictionsEnabled { get; set; } = false;

    /// <summary>
    /// Enable smart reorder suggestions
    /// </summary>
    public bool SmartReorderEnabled { get; set; } = false;

    /// <summary>
    /// Preferred language (en, hi, mr, ta)
    /// </summary>
    [StringLength(10)]
    public string Language { get; set; } = "en";

    /// <summary>
    /// Gemini API key for AI features
    /// </summary>
    [StringLength(200)]
    public string GeminiApiKey { get; set; }
}
