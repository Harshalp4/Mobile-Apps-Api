using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Bit2Sky.Bit2EHR.FinTrack.Dto;

namespace Bit2Sky.Bit2EHR.FinTrack;

/// <summary>
/// Application service interface for FinTrack personal finance mobile app
/// </summary>
public interface IFinTrackAppService : IApplicationService
{
    // ============ Core Sync APIs ============

    /// <summary>
    /// Sync bank accounts from mobile to server
    /// </summary>
    Task<FtSyncResult> SyncBankAccounts(SyncBankAccountsInput input);

    /// <summary>
    /// Sync categories from mobile to server
    /// </summary>
    Task<FtSyncResult> SyncCategories(SyncCategoriesInput input);

    /// <summary>
    /// Sync transactions from mobile to server
    /// </summary>
    Task<FtSyncResult> SyncTransactions(SyncTransactionsInput input);

    /// <summary>
    /// Sync assets from mobile to server
    /// </summary>
    Task<FtSyncResult> SyncAssets(SyncAssetsInput input);

    /// <summary>
    /// Sync liabilities from mobile to server
    /// </summary>
    Task<FtSyncResult> SyncLiabilities(SyncLiabilitiesInput input);

    /// <summary>
    /// Sync parties from mobile to server
    /// </summary>
    Task<FtSyncResult> SyncParties(SyncPartiesInput input);

    /// <summary>
    /// Sync expected incomes from mobile to server
    /// </summary>
    Task<FtSyncResult> SyncExpectedIncomes(SyncExpectedIncomesInput input);

    /// <summary>
    /// Sync fixed expenses from mobile to server
    /// </summary>
    Task<FtSyncResult> SyncFixedExpenses(SyncFixedExpensesInput input);

    /// <summary>
    /// Sync budget categories from mobile to server
    /// </summary>
    Task<FtSyncResult> SyncBudgetCategories(SyncBudgetCategoriesInput input);

    /// <summary>
    /// Sync all data at once from mobile to server
    /// </summary>
    Task<FtSyncResult> SyncAllData(SyncAllDataInput input);

    /// <summary>
    /// Pull all data from server for mobile sync
    /// </summary>
    Task<FtPullDataOutput> PullData(DateTime? lastSync);

    // ============ Dashboard & Analytics APIs ============

    /// <summary>
    /// Get dashboard summary data
    /// </summary>
    Task<FtDashboardDto> GetDashboard(GetFtDashboardInput input);

    /// <summary>
    /// Get monthly report
    /// </summary>
    Task<FtMonthlyReportDto> GetMonthlyReport(int month, int year);

    // ============ Individual Entity APIs ============

    /// <summary>
    /// Get all bank accounts
    /// </summary>
    Task<List<FtBankAccountDto>> GetBankAccounts();

    /// <summary>
    /// Get all categories
    /// </summary>
    Task<List<FtCategoryDto>> GetCategories();

    /// <summary>
    /// Get transactions with optional date filter
    /// </summary>
    Task<List<FtTransactionDto>> GetTransactions(DateTime? startDate, DateTime? endDate);

    /// <summary>
    /// Get all assets
    /// </summary>
    Task<List<FtAssetDto>> GetAssets();

    /// <summary>
    /// Get all liabilities
    /// </summary>
    Task<List<FtLiabilityDto>> GetLiabilities();

    /// <summary>
    /// Get all parties
    /// </summary>
    Task<List<FtPartyDto>> GetParties();

    /// <summary>
    /// Get all expected incomes
    /// </summary>
    Task<List<FtExpectedIncomeDto>> GetExpectedIncomes();

    /// <summary>
    /// Get fixed expenses for a month
    /// </summary>
    Task<List<FtFixedExpenseDto>> GetFixedExpenses(int month, int year);

    /// <summary>
    /// Get budget categories for a month
    /// </summary>
    Task<List<FtBudgetCategoryDto>> GetBudgetCategories(int month, int year);

    // ============ Delete APIs ============

    /// <summary>
    /// Delete specific records by client IDs
    /// </summary>
    Task<FtDeleteRecordsResult> DeleteRecords(FtDeleteRecordsInput input);

    /// <summary>
    /// Delete all FinTrack data (factory reset)
    /// </summary>
    Task<FtDeleteRecordsResult> DeleteAllData(FtDeleteAllDataInput input);
}
