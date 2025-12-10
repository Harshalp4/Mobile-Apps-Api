using System;
using System.Collections.Generic;

namespace Bit2Sky.Bit2EHR.FinTrack.Dto;

// ============ Sync Input DTOs ============

public class SyncBankAccountsInput
{
    public List<CreateOrUpdateFtBankAccountDto> Accounts { get; set; }
}

public class SyncCategoriesInput
{
    public List<CreateOrUpdateFtCategoryDto> Categories { get; set; }
}

public class SyncTransactionsInput
{
    public List<CreateOrUpdateFtTransactionDto> Transactions { get; set; }
}

public class SyncAssetsInput
{
    public List<CreateOrUpdateFtAssetDto> Assets { get; set; }
}

public class SyncLiabilitiesInput
{
    public List<CreateOrUpdateFtLiabilityDto> Liabilities { get; set; }
}

public class SyncPartiesInput
{
    public List<CreateOrUpdateFtPartyDto> Parties { get; set; }
}

public class SyncExpectedIncomesInput
{
    public List<CreateOrUpdateFtExpectedIncomeDto> ExpectedIncomes { get; set; }
}

public class SyncFixedExpensesInput
{
    public List<CreateOrUpdateFtFixedExpenseDto> FixedExpenses { get; set; }
}

public class SyncBudgetCategoriesInput
{
    public List<CreateOrUpdateFtBudgetCategoryDto> BudgetCategories { get; set; }
}

// ============ Sync All Data Input ============

public class SyncAllDataInput
{
    public List<CreateOrUpdateFtBankAccountDto> BankAccounts { get; set; }
    public List<CreateOrUpdateFtCategoryDto> Categories { get; set; }
    public List<CreateOrUpdateFtTransactionDto> Transactions { get; set; }
    public List<CreateOrUpdateFtAssetDto> Assets { get; set; }
    public List<CreateOrUpdateFtLiabilityDto> Liabilities { get; set; }
    public List<CreateOrUpdateFtPartyDto> Parties { get; set; }
    public List<CreateOrUpdateFtExpectedIncomeDto> ExpectedIncomes { get; set; }
    public List<CreateOrUpdateFtFixedExpenseDto> FixedExpenses { get; set; }
    public List<CreateOrUpdateFtBudgetCategoryDto> BudgetCategories { get; set; }
}

// ============ Pull Data Output ============

public class FtPullDataOutput
{
    public List<FtBankAccountDto> BankAccounts { get; set; }
    public List<FtCategoryDto> Categories { get; set; }
    public List<FtTransactionDto> Transactions { get; set; }
    public List<FtAssetDto> Assets { get; set; }
    public List<FtLiabilityDto> Liabilities { get; set; }
    public List<FtPartyDto> Parties { get; set; }
    public List<FtExpectedIncomeDto> ExpectedIncomes { get; set; }
    public List<FtFixedExpenseDto> FixedExpenses { get; set; }
    public List<FtBudgetCategoryDto> BudgetCategories { get; set; }
    public DateTime ServerTime { get; set; }
}

// ============ Sync Result ============

public class FtSyncResult
{
    public bool Success { get; set; }
    public int SyncedCount { get; set; }
    public List<FtSyncedRecordInfo> SyncedRecords { get; set; }
    public string ErrorMessage { get; set; }
}

public class FtSyncedRecordInfo
{
    public string ClientId { get; set; }
    public Guid ServerId { get; set; }
    public DateTime SyncedAt { get; set; }
}

// ============ Delete DTOs ============

public class FtDeleteRecordsInput
{
    public List<string> BankAccountClientIds { get; set; }
    public List<string> CategoryClientIds { get; set; }
    public List<string> TransactionClientIds { get; set; }
    public List<string> AssetClientIds { get; set; }
    public List<string> LiabilityClientIds { get; set; }
    public List<string> PartyClientIds { get; set; }
    public List<string> ExpectedIncomeClientIds { get; set; }
    public List<string> FixedExpenseClientIds { get; set; }
    public List<string> BudgetCategoryClientIds { get; set; }
}

public class FtDeleteRecordsResult
{
    public bool Success { get; set; }
    public int DeletedCount { get; set; }
    public string ErrorMessage { get; set; }
}

public class FtDeleteAllDataInput
{
    public bool ConfirmDelete { get; set; }
}
