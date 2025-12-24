using System;
using Abp.Application.Services.Dto;

namespace Bit2Sky.Bit2EHR.ChaiCount.Dto;

/// <summary>
/// DTO for shop settings
/// </summary>
public class ChaiCountShopSettingsDto : EntityDto<Guid>
{
    public long? UserId { get; set; }
    public string ClientId { get; set; }
    public string ShopName { get; set; }
    public string OwnerName { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public string UpiId { get; set; }
    public string GoogleReviewLink { get; set; }
    public string ReportEmail { get; set; }
    public string CurrencySymbol { get; set; }
    public string Timezone { get; set; }
    public DateTime? LastSyncedAt { get; set; }

    // Feature Toggles
    public bool AiEnabled { get; set; }
    public bool VoiceCommandsEnabled { get; set; }
    public bool WhatsAppEnabled { get; set; }
    public string WhatsAppNumber { get; set; }
    public bool DailyReportEnabled { get; set; }
    public string DailyReportTime { get; set; }
    public bool LowStockAlertsEnabled { get; set; }
    public bool SalesPredictionsEnabled { get; set; }
    public bool SmartReorderEnabled { get; set; }
    public string Language { get; set; }
    public string GeminiApiKey { get; set; }
}

/// <summary>
/// Input DTO for syncing shop settings
/// </summary>
public class SyncShopSettingsInput
{
    public long? UserId { get; set; }
    public string ClientId { get; set; }
    public string ShopName { get; set; }
    public string OwnerName { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public string UpiId { get; set; }
    public string GoogleReviewLink { get; set; }
    public string ReportEmail { get; set; }
    public string CurrencySymbol { get; set; }
    public string Timezone { get; set; }

    // Feature Toggles
    public bool AiEnabled { get; set; }
    public bool VoiceCommandsEnabled { get; set; }
    public bool WhatsAppEnabled { get; set; }
    public string WhatsAppNumber { get; set; }
    public bool DailyReportEnabled { get; set; }
    public string DailyReportTime { get; set; }
    public bool LowStockAlertsEnabled { get; set; }
    public bool SalesPredictionsEnabled { get; set; }
    public bool SmartReorderEnabled { get; set; }
    public string Language { get; set; }
    public string GeminiApiKey { get; set; }
}
