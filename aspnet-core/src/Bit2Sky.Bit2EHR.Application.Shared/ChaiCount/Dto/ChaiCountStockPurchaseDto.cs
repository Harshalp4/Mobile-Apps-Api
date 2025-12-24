using System;
using System.Collections.Generic;

namespace Bit2Sky.Bit2EHR.ChaiCount.Dto;

/// <summary>
/// DTO for stock purchase (expense) records
/// </summary>
public class ChaiCountStockPurchaseDto
{
    public Guid Id { get; set; }
    public string ClientId { get; set; }
    public Guid? InventoryItemId { get; set; }
    public string InventoryItemName { get; set; }
    public string Emoji { get; set; }
    public decimal Quantity { get; set; }
    public string Unit { get; set; }
    public decimal CostPerUnit { get; set; }
    public decimal TotalCost { get; set; }
    public DateTime PurchaseDate { get; set; }
    public string Note { get; set; }
    public DateTime LastSyncedAt { get; set; }
}

/// <summary>
/// Input for syncing stock purchases from mobile
/// </summary>
public class SyncStockPurchasesInput
{
    public List<ChaiCountStockPurchaseDto> Purchases { get; set; }
}

/// <summary>
/// Input for getting profit report
/// </summary>
public class GetProfitReportInput
{
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
}

/// <summary>
/// Output for profit report
/// </summary>
public class ProfitReportDto
{
    /// <summary>
    /// Total revenue from sales
    /// </summary>
    public decimal TotalRevenue { get; set; }

    /// <summary>
    /// Total expenses from stock purchases
    /// </summary>
    public decimal TotalExpenses { get; set; }

    /// <summary>
    /// Net profit (Revenue - Expenses)
    /// </summary>
    public decimal Profit { get; set; }

    /// <summary>
    /// Profit margin percentage
    /// </summary>
    public decimal ProfitMargin { get; set; }

    /// <summary>
    /// Revenue breakdown by item
    /// </summary>
    public List<RevenueByItemDto> RevenueByItem { get; set; }

    /// <summary>
    /// Expense breakdown by inventory item
    /// </summary>
    public List<ExpenseByItemDto> ExpenseByItem { get; set; }

    /// <summary>
    /// Daily profit trend
    /// </summary>
    public List<DailyProfitDto> DailyTrend { get; set; }
}

public class RevenueByItemDto
{
    public string ItemName { get; set; }
    public string Emoji { get; set; }
    public int QuantitySold { get; set; }
    public decimal Revenue { get; set; }
}

public class ExpenseByItemDto
{
    public string ItemName { get; set; }
    public string Emoji { get; set; }
    public decimal QuantityPurchased { get; set; }
    public string Unit { get; set; }
    public decimal TotalCost { get; set; }
}

public class DailyProfitDto
{
    public DateTime Date { get; set; }
    public decimal Revenue { get; set; }
    public decimal Expenses { get; set; }
    public decimal Profit { get; set; }
}
