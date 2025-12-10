using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Bit2Sky.Bit2EHR.ChaiCount.Dto;

namespace Bit2Sky.Bit2EHR.ChaiCount;

/// <summary>
/// Application service interface for ChaiCount mobile app
/// </summary>
public interface IChaiCountAppService : IApplicationService
{
    // ============ Core Sync APIs ============

    /// <summary>
    /// Sync items from mobile to server
    /// </summary>
    Task<SyncResult> SyncItems(SyncItemsInput input);

    /// <summary>
    /// Sync sales from mobile to server
    /// </summary>
    Task<SyncResult> SyncSales(SyncSalesInput input);

    /// <summary>
    /// Sync customers from mobile to server
    /// </summary>
    Task<SyncResult> SyncCustomers(SyncCustomersInput input);

    /// <summary>
    /// Sync inventory from mobile to server
    /// </summary>
    Task<SyncResult> SyncInventory(SyncInventoryInput input);

    /// <summary>
    /// Pull all data from server for mobile sync
    /// </summary>
    Task<PullDataOutput> PullData(DateTime? lastSync);

    /// <summary>
    /// Get low stock alerts
    /// </summary>
    Task<LowStockAlertDto> GetLowStockAlerts();

    /// <summary>
    /// Send daily report email
    /// </summary>
    Task SendDailyReport(SendDailyReportInput input);

    // ============ Settings & Configuration APIs ============

    /// <summary>
    /// Sync shop settings from mobile to server
    /// </summary>
    Task<ChaiCountShopSettingsDto> SyncSettings(SyncShopSettingsInput input);

    /// <summary>
    /// Get shop settings
    /// </summary>
    Task<ChaiCountShopSettingsDto> GetSettings();

    /// <summary>
    /// Sync loyalty configuration
    /// </summary>
    Task<ChaiCountLoyaltyConfigDto> SyncLoyaltyConfig(SyncLoyaltyConfigInput input);

    /// <summary>
    /// Get loyalty configuration
    /// </summary>
    Task<ChaiCountLoyaltyConfigDto> GetLoyaltyConfig();

    // ============ Offers APIs ============

    /// <summary>
    /// Sync offers from mobile to server
    /// </summary>
    Task<SyncResult> SyncOffers(SyncOffersInput input);

    /// <summary>
    /// Get all offers
    /// </summary>
    Task<List<ChaiCountOfferDto>> GetOffers(bool? activeOnly);

    // ============ Item Usage APIs ============

    /// <summary>
    /// Sync item usage mappings
    /// </summary>
    Task<SyncResult> SyncItemUsage(SyncItemUsageInput input);

    /// <summary>
    /// Get all item usage mappings
    /// </summary>
    Task<List<ChaiCountItemUsageDto>> GetItemUsage();

    // ============ Dashboard & Analytics APIs ============

    /// <summary>
    /// Get dashboard analytics data
    /// </summary>
    Task<ChaiCountDashboardDto> GetDashboard(GetDashboardInput input);

    // ============ Delete APIs ============

    /// <summary>
    /// Delete specific records by client IDs
    /// </summary>
    Task<DeleteRecordsResult> DeleteRecords(DeleteRecordsInput input);

    /// <summary>
    /// Delete all ChaiCount data (factory reset)
    /// </summary>
    Task<DeleteRecordsResult> DeleteAllData(DeleteAllDataInput input);

    // ============ Export APIs ============

    /// <summary>
    /// Export all data
    /// </summary>
    Task<ExportDataOutput> ExportData(ExportDataInput input);
}
