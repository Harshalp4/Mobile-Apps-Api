using System;
using System.Collections.Generic;

namespace Bit2Sky.Bit2EHR.FinTrack.Dto;

public class FtDashboardDto
{
    // Summary
    public decimal TotalBankBalance { get; set; }
    public decimal TotalAssets { get; set; }
    public decimal TotalLiabilities { get; set; }
    public decimal TotalOwesYou { get; set; }
    public decimal TotalYouOwe { get; set; }
    public decimal NetWorth { get; set; }

    // Current Month
    public decimal CurrentMonthIncome { get; set; }
    public decimal CurrentMonthExpense { get; set; }
    public decimal CurrentMonthNet { get; set; }

    // Recent Transactions
    public List<FtTransactionDto> RecentTransactions { get; set; }

    // Upcoming
    public List<FtExpectedIncomeDto> UpcomingIncome { get; set; }
    public List<FtFixedExpenseDto> UpcomingExpenses { get; set; }

    // Budget Overview
    public List<FtBudgetSummaryDto> BudgetSummary { get; set; }
}

public class FtBudgetSummaryDto
{
    public string CategoryName { get; set; }
    public decimal Allocated { get; set; }
    public decimal Spent { get; set; }
    public decimal Remaining { get; set; }
    public double PercentUsed { get; set; }
}

public class GetFtDashboardInput
{
    public int? Month { get; set; }
    public int? Year { get; set; }
}

public class FtMonthlyReportDto
{
    public int Month { get; set; }
    public int Year { get; set; }
    public decimal TotalIncome { get; set; }
    public decimal TotalExpense { get; set; }
    public decimal NetFlow { get; set; }
    public List<FtCategorySpendingDto> SpendingByCategory { get; set; }
    public List<FtTransactionDto> Transactions { get; set; }
}

public class FtCategorySpendingDto
{
    public string CategoryName { get; set; }
    public string CategoryClientId { get; set; }
    public decimal Amount { get; set; }
    public double Percentage { get; set; }
}
