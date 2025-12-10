using System;
using System.Collections.Generic;

namespace Bit2Sky.Bit2EHR.ChaiCount.Dto;

/// <summary>
/// Request DTO for syncing items from mobile to server
/// </summary>
public class SyncItemsInput
{
    public List<CreateOrUpdateChaiCountItemDto> Items { get; set; }
}

/// <summary>
/// Request DTO for syncing sales from mobile to server
/// </summary>
public class SyncSalesInput
{
    public List<CreateChaiCountSaleDto> Sales { get; set; }
}

/// <summary>
/// Request DTO for syncing customers from mobile to server
/// </summary>
public class SyncCustomersInput
{
    public List<CreateOrUpdateChaiCountCustomerDto> Customers { get; set; }
}

/// <summary>
/// Response DTO containing all data for mobile sync
/// </summary>
public class PullDataOutput
{
    public List<ChaiCountItemDto> Items { get; set; }
    public List<ChaiCountSaleDto> Sales { get; set; }
    public List<ChaiCountCustomerDto> Customers { get; set; }
    public List<ChaiCountInventoryDto> Inventory { get; set; }
    public DateTime ServerTime { get; set; }
}

/// <summary>
/// Request DTO for daily report email
/// </summary>
public class SendDailyReportInput
{
    public DateTime Date { get; set; }
    public DailyReportData ReportData { get; set; }
}

public class DailyReportData
{
    public string ShopName { get; set; }
    public decimal TotalRevenue { get; set; }
    public int TotalItems { get; set; }
    public int TotalTransactions { get; set; }
    public List<ItemSalesSummary> TopSellers { get; set; }
    public List<HourlySalesSummary> HourlySales { get; set; }
}

public class ItemSalesSummary
{
    public string ItemName { get; set; }
    public string Emoji { get; set; }
    public int Quantity { get; set; }
    public decimal Revenue { get; set; }
}

public class HourlySalesSummary
{
    public int Hour { get; set; }
    public int Count { get; set; }
    public decimal Revenue { get; set; }
}

/// <summary>
/// Sync result containing server-side IDs for client records
/// </summary>
public class SyncResult
{
    public bool Success { get; set; }
    public int SyncedCount { get; set; }
    public List<SyncedRecordInfo> SyncedRecords { get; set; }
    public string ErrorMessage { get; set; }
}

public class SyncedRecordInfo
{
    public string ClientId { get; set; }
    public Guid ServerId { get; set; }
    public DateTime SyncedAt { get; set; }
}
