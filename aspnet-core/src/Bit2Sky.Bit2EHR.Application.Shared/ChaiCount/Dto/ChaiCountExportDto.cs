using System;
using System.Collections.Generic;

namespace Bit2Sky.Bit2EHR.ChaiCount.Dto;

/// <summary>
/// Input for export request
/// </summary>
public class ExportDataInput
{
    /// <summary>
    /// Export format: json, csv
    /// </summary>
    public string Format { get; set; } = "json";

    /// <summary>
    /// What to export: all, items, sales, customers, inventory, settings
    /// </summary>
    public string DataType { get; set; } = "all";

    /// <summary>
    /// Date range filter for sales
    /// </summary>
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}

/// <summary>
/// Export result containing all data
/// </summary>
public class ExportDataOutput
{
    public string Format { get; set; }
    public DateTime ExportedAt { get; set; }
    public string ShopName { get; set; }

    // Data sections
    public ChaiCountShopSettingsDto Settings { get; set; }
    public ChaiCountLoyaltyConfigDto LoyaltyConfig { get; set; }
    public List<ChaiCountItemDto> Items { get; set; }
    public List<ChaiCountCustomerDto> Customers { get; set; }
    public List<ChaiCountInventoryDto> Inventory { get; set; }
    public List<ChaiCountSaleDto> Sales { get; set; }
    public List<ChaiCountOfferDto> Offers { get; set; }
    public List<ChaiCountItemUsageDto> ItemUsageMappings { get; set; }

    // Summary stats
    public ExportSummary Summary { get; set; }
}

public class ExportSummary
{
    public int TotalItems { get; set; }
    public int TotalCustomers { get; set; }
    public int TotalSales { get; set; }
    public int TotalInventoryItems { get; set; }
    public int TotalOffers { get; set; }
    public decimal TotalRevenue { get; set; }
}

/// <summary>
/// CSV export result
/// </summary>
public class CsvExportOutput
{
    public string FileName { get; set; }
    public string ContentType { get; set; }
    public byte[] Data { get; set; }
}
