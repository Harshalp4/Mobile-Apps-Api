using System;
using System.Collections.Generic;

namespace Bit2Sky.Bit2EHR.ChaiCount.Dto;

/// <summary>
/// Dashboard data for analytics
/// </summary>
public class ChaiCountDashboardDto
{
    // Today's summary
    public decimal TodayRevenue { get; set; }
    public int TodayItemsSold { get; set; }
    public int TodayTransactions { get; set; }
    public int TodayNewCustomers { get; set; }

    // This week
    public decimal WeekRevenue { get; set; }
    public int WeekItemsSold { get; set; }

    // This month
    public decimal MonthRevenue { get; set; }
    public int MonthItemsSold { get; set; }

    // All time
    public decimal TotalRevenue { get; set; }
    public int TotalItemsSold { get; set; }
    public int TotalCustomers { get; set; }

    // Top sellers
    public List<TopSellerDto> TopSellers { get; set; }

    // Revenue by day (last 7 days)
    public List<DailyRevenueDto> DailyRevenue { get; set; }

    // Inventory alerts
    public int LowStockCount { get; set; }
    public List<ChaiCountInventoryDto> LowStockItems { get; set; }

    // Customer insights
    public List<TopCustomerDto> TopCustomers { get; set; }

    // Trends
    public decimal RevenueGrowthPercent { get; set; }
    public decimal ItemsGrowthPercent { get; set; }

    public DateTime GeneratedAt { get; set; }
}

public class TopSellerDto
{
    public string ItemName { get; set; }
    public string Emoji { get; set; }
    public int TotalQuantity { get; set; }
    public decimal TotalRevenue { get; set; }
    public decimal Percentage { get; set; }
}

public class DailyRevenueDto
{
    public string Date { get; set; }
    public decimal Revenue { get; set; }
    public int ItemsSold { get; set; }
    public int Transactions { get; set; }
}

public class TopCustomerDto
{
    public string Name { get; set; }
    public string Phone { get; set; }
    public int TotalVisits { get; set; }
    public decimal TotalSpent { get; set; }
    public int LoyaltyPoints { get; set; }
}

/// <summary>
/// Input for dashboard request
/// </summary>
public class GetDashboardInput
{
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}
