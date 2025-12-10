using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Microsoft.EntityFrameworkCore;
using Bit2Sky.Bit2EHR.FinTrack.Dto;

namespace Bit2Sky.Bit2EHR.FinTrack;

/// <summary>
/// Application service for FinTrack personal finance mobile app
/// Handles sync, data management, and financial analytics
/// </summary>
[AbpAuthorize]
public class FinTrackAppService : Bit2EHRAppServiceBase, IFinTrackAppService
{
    private readonly IRepository<FtBankAccount, Guid> _bankAccountRepository;
    private readonly IRepository<FtCategory, Guid> _categoryRepository;
    private readonly IRepository<FtTransaction, Guid> _transactionRepository;
    private readonly IRepository<FtAsset, Guid> _assetRepository;
    private readonly IRepository<FtLiability, Guid> _liabilityRepository;
    private readonly IRepository<FtParty, Guid> _partyRepository;
    private readonly IRepository<FtExpectedIncome, Guid> _expectedIncomeRepository;
    private readonly IRepository<FtFixedExpense, Guid> _fixedExpenseRepository;
    private readonly IRepository<FtBudgetCategory, Guid> _budgetCategoryRepository;

    public FinTrackAppService(
        IRepository<FtBankAccount, Guid> bankAccountRepository,
        IRepository<FtCategory, Guid> categoryRepository,
        IRepository<FtTransaction, Guid> transactionRepository,
        IRepository<FtAsset, Guid> assetRepository,
        IRepository<FtLiability, Guid> liabilityRepository,
        IRepository<FtParty, Guid> partyRepository,
        IRepository<FtExpectedIncome, Guid> expectedIncomeRepository,
        IRepository<FtFixedExpense, Guid> fixedExpenseRepository,
        IRepository<FtBudgetCategory, Guid> budgetCategoryRepository)
    {
        _bankAccountRepository = bankAccountRepository;
        _categoryRepository = categoryRepository;
        _transactionRepository = transactionRepository;
        _assetRepository = assetRepository;
        _liabilityRepository = liabilityRepository;
        _partyRepository = partyRepository;
        _expectedIncomeRepository = expectedIncomeRepository;
        _fixedExpenseRepository = fixedExpenseRepository;
        _budgetCategoryRepository = budgetCategoryRepository;
    }

    // ============ Core Sync APIs ============

    public async Task<FtSyncResult> SyncBankAccounts(SyncBankAccountsInput input)
    {
        var result = new FtSyncResult { SyncedRecords = new List<FtSyncedRecordInfo>() };

        try
        {
            var tenantId = AbpSession.GetTenantId();

            foreach (var dto in input.Accounts)
            {
                var existing = await _bankAccountRepository.FirstOrDefaultAsync(
                    x => x.ClientId == dto.ClientId && x.TenantId == tenantId);

                if (existing != null)
                {
                    if (dto.LastSyncedAt > existing.LastSyncedAt)
                    {
                        existing.Name = dto.Name;
                        existing.AccountType = dto.AccountType;
                        existing.Balance = dto.Balance;
                        existing.OpeningBalance = dto.OpeningBalance;
                        existing.IsDefault = dto.IsDefault;
                        existing.DisplayOrder = dto.DisplayOrder;
                        existing.LastSyncedAt = DateTime.UtcNow;
                        await _bankAccountRepository.UpdateAsync(existing);
                    }

                    result.SyncedRecords.Add(new FtSyncedRecordInfo
                    {
                        ClientId = dto.ClientId,
                        ServerId = existing.Id,
                        SyncedAt = DateTime.UtcNow
                    });
                }
                else
                {
                    var newEntity = new FtBankAccount(
                        Guid.NewGuid(), tenantId, dto.ClientId, dto.Name, dto.AccountType, dto.OpeningBalance)
                    {
                        Balance = dto.Balance,
                        IsDefault = dto.IsDefault,
                        DisplayOrder = dto.DisplayOrder
                    };
                    await _bankAccountRepository.InsertAsync(newEntity);

                    result.SyncedRecords.Add(new FtSyncedRecordInfo
                    {
                        ClientId = dto.ClientId,
                        ServerId = newEntity.Id,
                        SyncedAt = DateTime.UtcNow
                    });
                }
            }

            result.Success = true;
            result.SyncedCount = result.SyncedRecords.Count;
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.ErrorMessage = ex.Message;
            Logger.Error("Error syncing bank accounts", ex);
        }

        return result;
    }

    public async Task<FtSyncResult> SyncCategories(SyncCategoriesInput input)
    {
        var result = new FtSyncResult { SyncedRecords = new List<FtSyncedRecordInfo>() };

        try
        {
            var tenantId = AbpSession.GetTenantId();

            foreach (var dto in input.Categories)
            {
                var existing = await _categoryRepository.FirstOrDefaultAsync(
                    x => x.ClientId == dto.ClientId && x.TenantId == tenantId);

                if (existing != null)
                {
                    if (dto.LastSyncedAt > existing.LastSyncedAt)
                    {
                        existing.Name = dto.Name;
                        existing.Icon = dto.Icon;
                        existing.Color = dto.Color;
                        existing.CategoryType = dto.CategoryType;
                        existing.IsSystem = dto.IsSystem;
                        existing.DisplayOrder = dto.DisplayOrder;
                        existing.LastSyncedAt = DateTime.UtcNow;
                        await _categoryRepository.UpdateAsync(existing);
                    }

                    result.SyncedRecords.Add(new FtSyncedRecordInfo
                    {
                        ClientId = dto.ClientId,
                        ServerId = existing.Id,
                        SyncedAt = DateTime.UtcNow
                    });
                }
                else
                {
                    var newEntity = new FtCategory(
                        Guid.NewGuid(), tenantId, dto.ClientId, dto.Name, dto.CategoryType, dto.Icon, dto.Color)
                    {
                        IsSystem = dto.IsSystem,
                        DisplayOrder = dto.DisplayOrder
                    };
                    await _categoryRepository.InsertAsync(newEntity);

                    result.SyncedRecords.Add(new FtSyncedRecordInfo
                    {
                        ClientId = dto.ClientId,
                        ServerId = newEntity.Id,
                        SyncedAt = DateTime.UtcNow
                    });
                }
            }

            result.Success = true;
            result.SyncedCount = result.SyncedRecords.Count;
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.ErrorMessage = ex.Message;
            Logger.Error("Error syncing categories", ex);
        }

        return result;
    }

    public async Task<FtSyncResult> SyncTransactions(SyncTransactionsInput input)
    {
        var result = new FtSyncResult { SyncedRecords = new List<FtSyncedRecordInfo>() };

        try
        {
            var tenantId = AbpSession.GetTenantId();

            foreach (var dto in input.Transactions)
            {
                var existing = await _transactionRepository.FirstOrDefaultAsync(
                    x => x.ClientId == dto.ClientId && x.TenantId == tenantId);

                // Resolve foreign keys by ClientId
                var account = await _bankAccountRepository.FirstOrDefaultAsync(
                    x => x.ClientId == dto.AccountClientId && x.TenantId == tenantId);
                var category = !string.IsNullOrEmpty(dto.CategoryClientId)
                    ? await _categoryRepository.FirstOrDefaultAsync(
                        x => x.ClientId == dto.CategoryClientId && x.TenantId == tenantId)
                    : null;
                var party = !string.IsNullOrEmpty(dto.PartyClientId)
                    ? await _partyRepository.FirstOrDefaultAsync(
                        x => x.ClientId == dto.PartyClientId && x.TenantId == tenantId)
                    : null;

                if (existing != null)
                {
                    if (dto.LastSyncedAt > existing.LastSyncedAt)
                    {
                        existing.Description = dto.Description;
                        existing.Amount = dto.Amount;
                        existing.TransactionType = dto.TransactionType;
                        existing.PaymentMode = dto.PaymentMode;
                        existing.TransactionDate = dto.TransactionDate;
                        existing.AccountId = account?.Id ?? existing.AccountId;
                        existing.CategoryId = category?.Id;
                        existing.PartyId = party?.Id;
                        existing.Notes = dto.Notes;
                        existing.LastSyncedAt = DateTime.UtcNow;
                        await _transactionRepository.UpdateAsync(existing);
                    }

                    result.SyncedRecords.Add(new FtSyncedRecordInfo
                    {
                        ClientId = dto.ClientId,
                        ServerId = existing.Id,
                        SyncedAt = DateTime.UtcNow
                    });
                }
                else
                {
                    var newEntity = new FtTransaction(
                        Guid.NewGuid(), tenantId, dto.ClientId, dto.Description, dto.Amount,
                        dto.TransactionType, dto.PaymentMode, dto.TransactionDate,
                        account?.Id ?? Guid.Empty)
                    {
                        CategoryId = category?.Id,
                        PartyId = party?.Id,
                        Notes = dto.Notes
                    };
                    await _transactionRepository.InsertAsync(newEntity);

                    result.SyncedRecords.Add(new FtSyncedRecordInfo
                    {
                        ClientId = dto.ClientId,
                        ServerId = newEntity.Id,
                        SyncedAt = DateTime.UtcNow
                    });
                }
            }

            result.Success = true;
            result.SyncedCount = result.SyncedRecords.Count;
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.ErrorMessage = ex.Message;
            Logger.Error("Error syncing transactions", ex);
        }

        return result;
    }

    public async Task<FtSyncResult> SyncAssets(SyncAssetsInput input)
    {
        var result = new FtSyncResult { SyncedRecords = new List<FtSyncedRecordInfo>() };

        try
        {
            var tenantId = AbpSession.GetTenantId();

            foreach (var dto in input.Assets)
            {
                var existing = await _assetRepository.FirstOrDefaultAsync(
                    x => x.ClientId == dto.ClientId && x.TenantId == tenantId);

                if (existing != null)
                {
                    if (dto.LastSyncedAt > existing.LastSyncedAt)
                    {
                        existing.Name = dto.Name;
                        existing.AssetType = dto.AssetType;
                        existing.TotalValue = dto.TotalValue;
                        existing.PaidAmount = dto.PaidAmount;
                        existing.PendingAmount = dto.PendingAmount;
                        existing.PurchaseDate = dto.PurchaseDate;
                        existing.Notes = dto.Notes;
                        existing.LastSyncedAt = DateTime.UtcNow;
                        await _assetRepository.UpdateAsync(existing);
                    }

                    result.SyncedRecords.Add(new FtSyncedRecordInfo
                    {
                        ClientId = dto.ClientId,
                        ServerId = existing.Id,
                        SyncedAt = DateTime.UtcNow
                    });
                }
                else
                {
                    var newEntity = new FtAsset(
                        Guid.NewGuid(), tenantId, dto.ClientId, dto.Name, dto.AssetType, dto.TotalValue, dto.PaidAmount)
                    {
                        PurchaseDate = dto.PurchaseDate,
                        Notes = dto.Notes
                    };
                    await _assetRepository.InsertAsync(newEntity);

                    result.SyncedRecords.Add(new FtSyncedRecordInfo
                    {
                        ClientId = dto.ClientId,
                        ServerId = newEntity.Id,
                        SyncedAt = DateTime.UtcNow
                    });
                }
            }

            result.Success = true;
            result.SyncedCount = result.SyncedRecords.Count;
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.ErrorMessage = ex.Message;
            Logger.Error("Error syncing assets", ex);
        }

        return result;
    }

    public async Task<FtSyncResult> SyncLiabilities(SyncLiabilitiesInput input)
    {
        var result = new FtSyncResult { SyncedRecords = new List<FtSyncedRecordInfo>() };

        try
        {
            var tenantId = AbpSession.GetTenantId();

            foreach (var dto in input.Liabilities)
            {
                var existing = await _liabilityRepository.FirstOrDefaultAsync(
                    x => x.ClientId == dto.ClientId && x.TenantId == tenantId);

                if (existing != null)
                {
                    if (dto.LastSyncedAt > existing.LastSyncedAt)
                    {
                        existing.Name = dto.Name;
                        existing.LiabilityType = dto.LiabilityType;
                        existing.Principal = dto.Principal;
                        existing.PaidAmount = dto.PaidAmount;
                        existing.PendingAmount = dto.PendingAmount;
                        existing.InterestRate = dto.InterestRate;
                        existing.Emi = dto.Emi;
                        existing.EmiDueDay = dto.EmiDueDay;
                        existing.StartDate = dto.StartDate;
                        existing.EndDate = dto.EndDate;
                        existing.Notes = dto.Notes;
                        existing.LastSyncedAt = DateTime.UtcNow;
                        await _liabilityRepository.UpdateAsync(existing);
                    }

                    result.SyncedRecords.Add(new FtSyncedRecordInfo
                    {
                        ClientId = dto.ClientId,
                        ServerId = existing.Id,
                        SyncedAt = DateTime.UtcNow
                    });
                }
                else
                {
                    var newEntity = new FtLiability(
                        Guid.NewGuid(), tenantId, dto.ClientId, dto.Name, dto.LiabilityType, dto.Principal, dto.PaidAmount)
                    {
                        InterestRate = dto.InterestRate,
                        Emi = dto.Emi,
                        EmiDueDay = dto.EmiDueDay,
                        StartDate = dto.StartDate,
                        EndDate = dto.EndDate,
                        Notes = dto.Notes
                    };
                    await _liabilityRepository.InsertAsync(newEntity);

                    result.SyncedRecords.Add(new FtSyncedRecordInfo
                    {
                        ClientId = dto.ClientId,
                        ServerId = newEntity.Id,
                        SyncedAt = DateTime.UtcNow
                    });
                }
            }

            result.Success = true;
            result.SyncedCount = result.SyncedRecords.Count;
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.ErrorMessage = ex.Message;
            Logger.Error("Error syncing liabilities", ex);
        }

        return result;
    }

    public async Task<FtSyncResult> SyncParties(SyncPartiesInput input)
    {
        var result = new FtSyncResult { SyncedRecords = new List<FtSyncedRecordInfo>() };

        try
        {
            var tenantId = AbpSession.GetTenantId();

            foreach (var dto in input.Parties)
            {
                var existing = await _partyRepository.FirstOrDefaultAsync(
                    x => x.ClientId == dto.ClientId && x.TenantId == tenantId);

                if (existing != null)
                {
                    if (dto.LastSyncedAt > existing.LastSyncedAt)
                    {
                        existing.Name = dto.Name;
                        existing.Phone = dto.Phone;
                        existing.OwesYou = dto.OwesYou;
                        existing.YouOwe = dto.YouOwe;
                        existing.NetBalance = dto.NetBalance;
                        existing.Notes = dto.Notes;
                        existing.LastSyncedAt = DateTime.UtcNow;
                        await _partyRepository.UpdateAsync(existing);
                    }

                    result.SyncedRecords.Add(new FtSyncedRecordInfo
                    {
                        ClientId = dto.ClientId,
                        ServerId = existing.Id,
                        SyncedAt = DateTime.UtcNow
                    });
                }
                else
                {
                    var newEntity = new FtParty(
                        Guid.NewGuid(), tenantId, dto.ClientId, dto.Name, dto.Phone)
                    {
                        OwesYou = dto.OwesYou,
                        YouOwe = dto.YouOwe,
                        NetBalance = dto.NetBalance,
                        Notes = dto.Notes
                    };
                    await _partyRepository.InsertAsync(newEntity);

                    result.SyncedRecords.Add(new FtSyncedRecordInfo
                    {
                        ClientId = dto.ClientId,
                        ServerId = newEntity.Id,
                        SyncedAt = DateTime.UtcNow
                    });
                }
            }

            result.Success = true;
            result.SyncedCount = result.SyncedRecords.Count;
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.ErrorMessage = ex.Message;
            Logger.Error("Error syncing parties", ex);
        }

        return result;
    }

    public async Task<FtSyncResult> SyncExpectedIncomes(SyncExpectedIncomesInput input)
    {
        var result = new FtSyncResult { SyncedRecords = new List<FtSyncedRecordInfo>() };

        try
        {
            var tenantId = AbpSession.GetTenantId();

            foreach (var dto in input.ExpectedIncomes)
            {
                var existing = await _expectedIncomeRepository.FirstOrDefaultAsync(
                    x => x.ClientId == dto.ClientId && x.TenantId == tenantId);

                if (existing != null)
                {
                    if (dto.LastSyncedAt > existing.LastSyncedAt)
                    {
                        existing.Source = dto.Source;
                        existing.Amount = dto.Amount;
                        existing.Frequency = dto.Frequency;
                        existing.NextDate = dto.NextDate;
                        existing.DayOfMonth = dto.DayOfMonth;
                        existing.Status = dto.Status;
                        existing.IsActive = dto.IsActive;
                        existing.Notes = dto.Notes;
                        existing.LastSyncedAt = DateTime.UtcNow;
                        await _expectedIncomeRepository.UpdateAsync(existing);
                    }

                    result.SyncedRecords.Add(new FtSyncedRecordInfo
                    {
                        ClientId = dto.ClientId,
                        ServerId = existing.Id,
                        SyncedAt = DateTime.UtcNow
                    });
                }
                else
                {
                    var newEntity = new FtExpectedIncome(
                        Guid.NewGuid(), tenantId, dto.ClientId, dto.Source, dto.Amount, dto.Frequency, dto.NextDate)
                    {
                        DayOfMonth = dto.DayOfMonth,
                        Status = dto.Status,
                        IsActive = dto.IsActive,
                        Notes = dto.Notes
                    };
                    await _expectedIncomeRepository.InsertAsync(newEntity);

                    result.SyncedRecords.Add(new FtSyncedRecordInfo
                    {
                        ClientId = dto.ClientId,
                        ServerId = newEntity.Id,
                        SyncedAt = DateTime.UtcNow
                    });
                }
            }

            result.Success = true;
            result.SyncedCount = result.SyncedRecords.Count;
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.ErrorMessage = ex.Message;
            Logger.Error("Error syncing expected incomes", ex);
        }

        return result;
    }

    public async Task<FtSyncResult> SyncFixedExpenses(SyncFixedExpensesInput input)
    {
        var result = new FtSyncResult { SyncedRecords = new List<FtSyncedRecordInfo>() };

        try
        {
            var tenantId = AbpSession.GetTenantId();

            foreach (var dto in input.FixedExpenses)
            {
                var existing = await _fixedExpenseRepository.FirstOrDefaultAsync(
                    x => x.ClientId == dto.ClientId && x.TenantId == tenantId);

                if (existing != null)
                {
                    if (dto.LastSyncedAt > existing.LastSyncedAt)
                    {
                        existing.Name = dto.Name;
                        existing.Amount = dto.Amount;
                        existing.DueDate = dto.DueDate;
                        existing.Month = dto.Month;
                        existing.Year = dto.Year;
                        existing.Status = dto.Status;
                        existing.PaidDate = dto.PaidDate;
                        existing.IsRecurring = dto.IsRecurring;
                        existing.Notes = dto.Notes;
                        existing.LastSyncedAt = DateTime.UtcNow;
                        await _fixedExpenseRepository.UpdateAsync(existing);
                    }

                    result.SyncedRecords.Add(new FtSyncedRecordInfo
                    {
                        ClientId = dto.ClientId,
                        ServerId = existing.Id,
                        SyncedAt = DateTime.UtcNow
                    });
                }
                else
                {
                    var newEntity = new FtFixedExpense(
                        Guid.NewGuid(), tenantId, dto.ClientId, dto.Name, dto.Amount, dto.DueDate, dto.Month, dto.Year)
                    {
                        Status = dto.Status,
                        PaidDate = dto.PaidDate,
                        IsRecurring = dto.IsRecurring,
                        Notes = dto.Notes
                    };
                    await _fixedExpenseRepository.InsertAsync(newEntity);

                    result.SyncedRecords.Add(new FtSyncedRecordInfo
                    {
                        ClientId = dto.ClientId,
                        ServerId = newEntity.Id,
                        SyncedAt = DateTime.UtcNow
                    });
                }
            }

            result.Success = true;
            result.SyncedCount = result.SyncedRecords.Count;
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.ErrorMessage = ex.Message;
            Logger.Error("Error syncing fixed expenses", ex);
        }

        return result;
    }

    public async Task<FtSyncResult> SyncBudgetCategories(SyncBudgetCategoriesInput input)
    {
        var result = new FtSyncResult { SyncedRecords = new List<FtSyncedRecordInfo>() };

        try
        {
            var tenantId = AbpSession.GetTenantId();

            foreach (var dto in input.BudgetCategories)
            {
                var existing = await _budgetCategoryRepository.FirstOrDefaultAsync(
                    x => x.ClientId == dto.ClientId && x.TenantId == tenantId);

                var category = !string.IsNullOrEmpty(dto.CategoryClientId)
                    ? await _categoryRepository.FirstOrDefaultAsync(
                        x => x.ClientId == dto.CategoryClientId && x.TenantId == tenantId)
                    : null;

                if (existing != null)
                {
                    if (dto.LastSyncedAt > existing.LastSyncedAt)
                    {
                        existing.CategoryName = dto.CategoryName;
                        existing.CategoryId = category?.Id;
                        existing.AllocatedAmount = dto.AllocatedAmount;
                        existing.SpentAmount = dto.SpentAmount;
                        existing.Month = dto.Month;
                        existing.Year = dto.Year;
                        existing.LastSyncedAt = DateTime.UtcNow;
                        await _budgetCategoryRepository.UpdateAsync(existing);
                    }

                    result.SyncedRecords.Add(new FtSyncedRecordInfo
                    {
                        ClientId = dto.ClientId,
                        ServerId = existing.Id,
                        SyncedAt = DateTime.UtcNow
                    });
                }
                else
                {
                    var newEntity = new FtBudgetCategory(
                        Guid.NewGuid(), tenantId, dto.ClientId, dto.CategoryName, dto.AllocatedAmount, dto.Month, dto.Year)
                    {
                        CategoryId = category?.Id,
                        SpentAmount = dto.SpentAmount
                    };
                    await _budgetCategoryRepository.InsertAsync(newEntity);

                    result.SyncedRecords.Add(new FtSyncedRecordInfo
                    {
                        ClientId = dto.ClientId,
                        ServerId = newEntity.Id,
                        SyncedAt = DateTime.UtcNow
                    });
                }
            }

            result.Success = true;
            result.SyncedCount = result.SyncedRecords.Count;
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.ErrorMessage = ex.Message;
            Logger.Error("Error syncing budget categories", ex);
        }

        return result;
    }

    public async Task<FtSyncResult> SyncAllData(SyncAllDataInput input)
    {
        var result = new FtSyncResult { SyncedRecords = new List<FtSyncedRecordInfo>() };
        var totalSynced = 0;

        try
        {
            // Sync in order of dependencies: Categories & Accounts first, then Parties, then Transactions
            if (input.Categories?.Any() == true)
            {
                var catResult = await SyncCategories(new SyncCategoriesInput { Categories = input.Categories });
                totalSynced += catResult.SyncedCount;
                result.SyncedRecords.AddRange(catResult.SyncedRecords);
            }

            if (input.BankAccounts?.Any() == true)
            {
                var accResult = await SyncBankAccounts(new SyncBankAccountsInput { Accounts = input.BankAccounts });
                totalSynced += accResult.SyncedCount;
                result.SyncedRecords.AddRange(accResult.SyncedRecords);
            }

            if (input.Parties?.Any() == true)
            {
                var partyResult = await SyncParties(new SyncPartiesInput { Parties = input.Parties });
                totalSynced += partyResult.SyncedCount;
                result.SyncedRecords.AddRange(partyResult.SyncedRecords);
            }

            if (input.Transactions?.Any() == true)
            {
                var txResult = await SyncTransactions(new SyncTransactionsInput { Transactions = input.Transactions });
                totalSynced += txResult.SyncedCount;
                result.SyncedRecords.AddRange(txResult.SyncedRecords);
            }

            if (input.Assets?.Any() == true)
            {
                var assetResult = await SyncAssets(new SyncAssetsInput { Assets = input.Assets });
                totalSynced += assetResult.SyncedCount;
                result.SyncedRecords.AddRange(assetResult.SyncedRecords);
            }

            if (input.Liabilities?.Any() == true)
            {
                var libResult = await SyncLiabilities(new SyncLiabilitiesInput { Liabilities = input.Liabilities });
                totalSynced += libResult.SyncedCount;
                result.SyncedRecords.AddRange(libResult.SyncedRecords);
            }

            if (input.ExpectedIncomes?.Any() == true)
            {
                var incResult = await SyncExpectedIncomes(new SyncExpectedIncomesInput { ExpectedIncomes = input.ExpectedIncomes });
                totalSynced += incResult.SyncedCount;
                result.SyncedRecords.AddRange(incResult.SyncedRecords);
            }

            if (input.FixedExpenses?.Any() == true)
            {
                var expResult = await SyncFixedExpenses(new SyncFixedExpensesInput { FixedExpenses = input.FixedExpenses });
                totalSynced += expResult.SyncedCount;
                result.SyncedRecords.AddRange(expResult.SyncedRecords);
            }

            if (input.BudgetCategories?.Any() == true)
            {
                var budgetResult = await SyncBudgetCategories(new SyncBudgetCategoriesInput { BudgetCategories = input.BudgetCategories });
                totalSynced += budgetResult.SyncedCount;
                result.SyncedRecords.AddRange(budgetResult.SyncedRecords);
            }

            result.Success = true;
            result.SyncedCount = totalSynced;
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.ErrorMessage = ex.Message;
            Logger.Error("Error syncing all data", ex);
        }

        return result;
    }

    public async Task<FtPullDataOutput> PullData(DateTime? lastSync)
    {
        var tenantId = AbpSession.GetTenantId();

        var accountsQuery = _bankAccountRepository.GetAll().Where(x => x.TenantId == tenantId && !x.IsDeleted);
        var categoriesQuery = _categoryRepository.GetAll().Where(x => x.TenantId == tenantId && !x.IsDeleted);
        var transactionsQuery = _transactionRepository.GetAll().Where(x => x.TenantId == tenantId && !x.IsDeleted);
        var assetsQuery = _assetRepository.GetAll().Where(x => x.TenantId == tenantId && !x.IsDeleted);
        var liabilitiesQuery = _liabilityRepository.GetAll().Where(x => x.TenantId == tenantId && !x.IsDeleted);
        var partiesQuery = _partyRepository.GetAll().Where(x => x.TenantId == tenantId && !x.IsDeleted);
        var expectedIncomesQuery = _expectedIncomeRepository.GetAll().Where(x => x.TenantId == tenantId && !x.IsDeleted);
        var fixedExpensesQuery = _fixedExpenseRepository.GetAll().Where(x => x.TenantId == tenantId && !x.IsDeleted);
        var budgetCategoriesQuery = _budgetCategoryRepository.GetAll().Where(x => x.TenantId == tenantId && !x.IsDeleted);

        if (lastSync.HasValue)
        {
            accountsQuery = accountsQuery.Where(x => x.LastSyncedAt > lastSync.Value || x.LastModificationTime > lastSync.Value);
            categoriesQuery = categoriesQuery.Where(x => x.LastSyncedAt > lastSync.Value || x.LastModificationTime > lastSync.Value);
            transactionsQuery = transactionsQuery.Where(x => x.LastSyncedAt > lastSync.Value || x.LastModificationTime > lastSync.Value);
            assetsQuery = assetsQuery.Where(x => x.LastSyncedAt > lastSync.Value || x.LastModificationTime > lastSync.Value);
            liabilitiesQuery = liabilitiesQuery.Where(x => x.LastSyncedAt > lastSync.Value || x.LastModificationTime > lastSync.Value);
            partiesQuery = partiesQuery.Where(x => x.LastSyncedAt > lastSync.Value || x.LastModificationTime > lastSync.Value);
            expectedIncomesQuery = expectedIncomesQuery.Where(x => x.LastSyncedAt > lastSync.Value || x.LastModificationTime > lastSync.Value);
            fixedExpensesQuery = fixedExpensesQuery.Where(x => x.LastSyncedAt > lastSync.Value || x.LastModificationTime > lastSync.Value);
            budgetCategoriesQuery = budgetCategoriesQuery.Where(x => x.LastSyncedAt > lastSync.Value || x.LastModificationTime > lastSync.Value);
        }

        var accounts = await accountsQuery.ToListAsync();
        var categories = await categoriesQuery.ToListAsync();
        var transactions = await transactionsQuery.ToListAsync();
        var assets = await assetsQuery.ToListAsync();
        var liabilities = await liabilitiesQuery.ToListAsync();
        var parties = await partiesQuery.ToListAsync();
        var expectedIncomes = await expectedIncomesQuery.ToListAsync();
        var fixedExpenses = await fixedExpensesQuery.ToListAsync();
        var budgetCategories = await budgetCategoriesQuery.ToListAsync();

        // Build lookup dictionaries for foreign key resolution
        var accountLookup = accounts.ToDictionary(a => a.Id, a => a.ClientId);
        var categoryLookup = categories.ToDictionary(c => c.Id, c => c.ClientId);
        var partyLookup = parties.ToDictionary(p => p.Id, p => p.ClientId);

        return new FtPullDataOutput
        {
            BankAccounts = accounts.Select(x => new FtBankAccountDto
            {
                Id = x.Id,
                ClientId = x.ClientId,
                Name = x.Name,
                AccountType = x.AccountType,
                Balance = x.Balance,
                OpeningBalance = x.OpeningBalance,
                IsDefault = x.IsDefault,
                DisplayOrder = x.DisplayOrder,
                LastSyncedAt = x.LastSyncedAt,
                CreationTime = x.CreationTime,
                LastModificationTime = x.LastModificationTime
            }).ToList(),
            Categories = categories.Select(x => new FtCategoryDto
            {
                Id = x.Id,
                ClientId = x.ClientId,
                Name = x.Name,
                Icon = x.Icon,
                Color = x.Color,
                CategoryType = x.CategoryType,
                IsSystem = x.IsSystem,
                DisplayOrder = x.DisplayOrder,
                LastSyncedAt = x.LastSyncedAt,
                CreationTime = x.CreationTime,
                LastModificationTime = x.LastModificationTime
            }).ToList(),
            Transactions = transactions.Select(x => new FtTransactionDto
            {
                Id = x.Id,
                ClientId = x.ClientId,
                Description = x.Description,
                Amount = x.Amount,
                TransactionType = x.TransactionType,
                PaymentMode = x.PaymentMode,
                TransactionDate = x.TransactionDate,
                AccountClientId = accountLookup.GetValueOrDefault(x.AccountId),
                CategoryClientId = x.CategoryId.HasValue ? categoryLookup.GetValueOrDefault(x.CategoryId.Value) : null,
                PartyClientId = x.PartyId.HasValue ? partyLookup.GetValueOrDefault(x.PartyId.Value) : null,
                Notes = x.Notes,
                LastSyncedAt = x.LastSyncedAt,
                CreationTime = x.CreationTime,
                LastModificationTime = x.LastModificationTime
            }).ToList(),
            Assets = assets.Select(x => new FtAssetDto
            {
                Id = x.Id,
                ClientId = x.ClientId,
                Name = x.Name,
                AssetType = x.AssetType,
                TotalValue = x.TotalValue,
                PaidAmount = x.PaidAmount,
                PendingAmount = x.PendingAmount,
                PurchaseDate = x.PurchaseDate,
                Notes = x.Notes,
                LastSyncedAt = x.LastSyncedAt,
                CreationTime = x.CreationTime,
                LastModificationTime = x.LastModificationTime
            }).ToList(),
            Liabilities = liabilities.Select(x => new FtLiabilityDto
            {
                Id = x.Id,
                ClientId = x.ClientId,
                Name = x.Name,
                LiabilityType = x.LiabilityType,
                Principal = x.Principal,
                PaidAmount = x.PaidAmount,
                PendingAmount = x.PendingAmount,
                InterestRate = x.InterestRate,
                Emi = x.Emi,
                EmiDueDay = x.EmiDueDay,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                Notes = x.Notes,
                LastSyncedAt = x.LastSyncedAt,
                CreationTime = x.CreationTime,
                LastModificationTime = x.LastModificationTime
            }).ToList(),
            Parties = parties.Select(x => new FtPartyDto
            {
                Id = x.Id,
                ClientId = x.ClientId,
                Name = x.Name,
                Phone = x.Phone,
                OwesYou = x.OwesYou,
                YouOwe = x.YouOwe,
                NetBalance = x.NetBalance,
                Notes = x.Notes,
                LastSyncedAt = x.LastSyncedAt,
                CreationTime = x.CreationTime,
                LastModificationTime = x.LastModificationTime
            }).ToList(),
            ExpectedIncomes = expectedIncomes.Select(x => new FtExpectedIncomeDto
            {
                Id = x.Id,
                ClientId = x.ClientId,
                Source = x.Source,
                Amount = x.Amount,
                Frequency = x.Frequency,
                NextDate = x.NextDate,
                DayOfMonth = x.DayOfMonth,
                Status = x.Status,
                IsActive = x.IsActive,
                Notes = x.Notes,
                LastSyncedAt = x.LastSyncedAt,
                CreationTime = x.CreationTime,
                LastModificationTime = x.LastModificationTime
            }).ToList(),
            FixedExpenses = fixedExpenses.Select(x => new FtFixedExpenseDto
            {
                Id = x.Id,
                ClientId = x.ClientId,
                Name = x.Name,
                Amount = x.Amount,
                DueDate = x.DueDate,
                Month = x.Month,
                Year = x.Year,
                Status = x.Status,
                PaidDate = x.PaidDate,
                IsRecurring = x.IsRecurring,
                Notes = x.Notes,
                LastSyncedAt = x.LastSyncedAt,
                CreationTime = x.CreationTime,
                LastModificationTime = x.LastModificationTime
            }).ToList(),
            BudgetCategories = budgetCategories.Select(x => new FtBudgetCategoryDto
            {
                Id = x.Id,
                ClientId = x.ClientId,
                CategoryName = x.CategoryName,
                CategoryClientId = x.CategoryId.HasValue ? categoryLookup.GetValueOrDefault(x.CategoryId.Value) : null,
                AllocatedAmount = x.AllocatedAmount,
                SpentAmount = x.SpentAmount,
                Month = x.Month,
                Year = x.Year,
                LastSyncedAt = x.LastSyncedAt,
                CreationTime = x.CreationTime,
                LastModificationTime = x.LastModificationTime
            }).ToList(),
            ServerTime = DateTime.UtcNow
        };
    }

    // ============ Dashboard & Analytics APIs ============

    public async Task<FtDashboardDto> GetDashboard(GetFtDashboardInput input)
    {
        var tenantId = AbpSession.GetTenantId();
        var now = DateTime.UtcNow;
        var month = input?.Month ?? now.Month;
        var year = input?.Year ?? now.Year;

        var accounts = await _bankAccountRepository.GetAll()
            .Where(x => x.TenantId == tenantId && !x.IsDeleted).ToListAsync();
        var assets = await _assetRepository.GetAll()
            .Where(x => x.TenantId == tenantId && !x.IsDeleted).ToListAsync();
        var liabilities = await _liabilityRepository.GetAll()
            .Where(x => x.TenantId == tenantId && !x.IsDeleted).ToListAsync();
        var parties = await _partyRepository.GetAll()
            .Where(x => x.TenantId == tenantId && !x.IsDeleted).ToListAsync();
        var transactions = await _transactionRepository.GetAll()
            .Where(x => x.TenantId == tenantId && !x.IsDeleted).ToListAsync();
        var expectedIncomes = await _expectedIncomeRepository.GetAll()
            .Where(x => x.TenantId == tenantId && !x.IsDeleted && x.IsActive).ToListAsync();
        var fixedExpenses = await _fixedExpenseRepository.GetAll()
            .Where(x => x.TenantId == tenantId && !x.IsDeleted && x.Month == month && x.Year == year).ToListAsync();
        var budgetCategories = await _budgetCategoryRepository.GetAll()
            .Where(x => x.TenantId == tenantId && !x.IsDeleted && x.Month == month && x.Year == year).ToListAsync();

        var totalBankBalance = accounts.Sum(a => a.Balance);
        var totalAssets = assets.Sum(a => a.TotalValue);
        var totalLiabilities = liabilities.Sum(l => l.PendingAmount);
        var totalOwesYou = parties.Sum(p => p.OwesYou);
        var totalYouOwe = parties.Sum(p => p.YouOwe);
        var netWorth = totalBankBalance + totalAssets - totalLiabilities + totalOwesYou - totalYouOwe;

        var monthlyTx = transactions.Where(t =>
            t.TransactionDate.Month == month && t.TransactionDate.Year == year).ToList();
        var currentMonthIncome = monthlyTx.Where(t => t.TransactionType == "moneyIn").Sum(t => t.Amount);
        var currentMonthExpense = monthlyTx.Where(t => t.TransactionType == "moneyOut").Sum(t => t.Amount);

        var recentTx = transactions.OrderByDescending(t => t.TransactionDate).Take(10).ToList();

        return new FtDashboardDto
        {
            TotalBankBalance = totalBankBalance,
            TotalAssets = totalAssets,
            TotalLiabilities = totalLiabilities,
            TotalOwesYou = totalOwesYou,
            TotalYouOwe = totalYouOwe,
            NetWorth = netWorth,
            CurrentMonthIncome = currentMonthIncome,
            CurrentMonthExpense = currentMonthExpense,
            CurrentMonthNet = currentMonthIncome - currentMonthExpense,
            RecentTransactions = recentTx.Select(MapToTransactionDto).ToList(),
            UpcomingIncome = expectedIncomes
                .Where(i => i.NextDate >= now && i.NextDate <= now.AddDays(30))
                .OrderBy(i => i.NextDate)
                .Select(MapToExpectedIncomeDto).ToList(),
            UpcomingExpenses = fixedExpenses
                .Where(e => e.Status == "pending")
                .OrderBy(e => e.DueDate)
                .Select(MapToFixedExpenseDto).ToList(),
            BudgetSummary = budgetCategories.Select(b => new FtBudgetSummaryDto
            {
                CategoryName = b.CategoryName,
                Allocated = b.AllocatedAmount,
                Spent = b.SpentAmount,
                Remaining = b.AllocatedAmount - b.SpentAmount,
                PercentUsed = b.AllocatedAmount > 0 ? (double)(b.SpentAmount / b.AllocatedAmount * 100) : 0
            }).ToList()
        };
    }

    public async Task<FtMonthlyReportDto> GetMonthlyReport(int month, int year)
    {
        var tenantId = AbpSession.GetTenantId();

        var transactions = await _transactionRepository.GetAll()
            .Where(x => x.TenantId == tenantId && !x.IsDeleted &&
                        x.TransactionDate.Month == month && x.TransactionDate.Year == year)
            .ToListAsync();

        var categories = await _categoryRepository.GetAll()
            .Where(x => x.TenantId == tenantId && !x.IsDeleted).ToListAsync();
        var categoryLookup = categories.ToDictionary(c => c.Id, c => c);

        var totalIncome = transactions.Where(t => t.TransactionType == "moneyIn").Sum(t => t.Amount);
        var totalExpense = transactions.Where(t => t.TransactionType == "moneyOut").Sum(t => t.Amount);

        var spendingByCategory = transactions
            .Where(t => t.TransactionType == "moneyOut" && t.CategoryId.HasValue)
            .GroupBy(t => t.CategoryId.Value)
            .Select(g => new FtCategorySpendingDto
            {
                CategoryName = categoryLookup.ContainsKey(g.Key) ? categoryLookup[g.Key].Name : "Unknown",
                CategoryClientId = categoryLookup.ContainsKey(g.Key) ? categoryLookup[g.Key].ClientId : null,
                Amount = g.Sum(t => t.Amount),
                Percentage = totalExpense > 0 ? (double)(g.Sum(t => t.Amount) / totalExpense * 100) : 0
            })
            .OrderByDescending(c => c.Amount)
            .ToList();

        return new FtMonthlyReportDto
        {
            Month = month,
            Year = year,
            TotalIncome = totalIncome,
            TotalExpense = totalExpense,
            NetFlow = totalIncome - totalExpense,
            SpendingByCategory = spendingByCategory,
            Transactions = transactions.OrderByDescending(t => t.TransactionDate).Select(MapToTransactionDto).ToList()
        };
    }

    // ============ Individual Entity APIs ============

    public async Task<List<FtBankAccountDto>> GetBankAccounts()
    {
        var tenantId = AbpSession.GetTenantId();
        var accounts = await _bankAccountRepository.GetAll()
            .Where(x => x.TenantId == tenantId && !x.IsDeleted)
            .OrderBy(x => x.DisplayOrder)
            .ToListAsync();
        return accounts.Select(MapToBankAccountDto).ToList();
    }

    public async Task<List<FtCategoryDto>> GetCategories()
    {
        var tenantId = AbpSession.GetTenantId();
        var categories = await _categoryRepository.GetAll()
            .Where(x => x.TenantId == tenantId && !x.IsDeleted)
            .OrderBy(x => x.DisplayOrder)
            .ToListAsync();
        return categories.Select(MapToCategoryDto).ToList();
    }

    public async Task<List<FtTransactionDto>> GetTransactions(DateTime? startDate, DateTime? endDate)
    {
        var tenantId = AbpSession.GetTenantId();
        var query = _transactionRepository.GetAll()
            .Where(x => x.TenantId == tenantId && !x.IsDeleted);

        if (startDate.HasValue)
            query = query.Where(x => x.TransactionDate >= startDate.Value);
        if (endDate.HasValue)
            query = query.Where(x => x.TransactionDate <= endDate.Value);

        var transactions = await query.OrderByDescending(x => x.TransactionDate).ToListAsync();
        return transactions.Select(MapToTransactionDto).ToList();
    }

    public async Task<List<FtAssetDto>> GetAssets()
    {
        var tenantId = AbpSession.GetTenantId();
        var assets = await _assetRepository.GetAll()
            .Where(x => x.TenantId == tenantId && !x.IsDeleted)
            .ToListAsync();
        return assets.Select(MapToAssetDto).ToList();
    }

    public async Task<List<FtLiabilityDto>> GetLiabilities()
    {
        var tenantId = AbpSession.GetTenantId();
        var liabilities = await _liabilityRepository.GetAll()
            .Where(x => x.TenantId == tenantId && !x.IsDeleted)
            .ToListAsync();
        return liabilities.Select(MapToLiabilityDto).ToList();
    }

    public async Task<List<FtPartyDto>> GetParties()
    {
        var tenantId = AbpSession.GetTenantId();
        var parties = await _partyRepository.GetAll()
            .Where(x => x.TenantId == tenantId && !x.IsDeleted)
            .ToListAsync();
        return parties.Select(MapToPartyDto).ToList();
    }

    public async Task<List<FtExpectedIncomeDto>> GetExpectedIncomes()
    {
        var tenantId = AbpSession.GetTenantId();
        var incomes = await _expectedIncomeRepository.GetAll()
            .Where(x => x.TenantId == tenantId && !x.IsDeleted)
            .ToListAsync();
        return incomes.Select(MapToExpectedIncomeDto).ToList();
    }

    public async Task<List<FtFixedExpenseDto>> GetFixedExpenses(int month, int year)
    {
        var tenantId = AbpSession.GetTenantId();
        var expenses = await _fixedExpenseRepository.GetAll()
            .Where(x => x.TenantId == tenantId && !x.IsDeleted && x.Month == month && x.Year == year)
            .ToListAsync();
        return expenses.Select(MapToFixedExpenseDto).ToList();
    }

    public async Task<List<FtBudgetCategoryDto>> GetBudgetCategories(int month, int year)
    {
        var tenantId = AbpSession.GetTenantId();
        var budgets = await _budgetCategoryRepository.GetAll()
            .Where(x => x.TenantId == tenantId && !x.IsDeleted && x.Month == month && x.Year == year)
            .ToListAsync();
        return budgets.Select(MapToBudgetCategoryDto).ToList();
    }

    // ============ Delete APIs ============

    public async Task<FtDeleteRecordsResult> DeleteRecords(FtDeleteRecordsInput input)
    {
        var result = new FtDeleteRecordsResult();
        var deletedCount = 0;

        try
        {
            var tenantId = AbpSession.GetTenantId();

            if (input.BankAccountClientIds?.Any() == true)
            {
                foreach (var clientId in input.BankAccountClientIds)
                {
                    var entity = await _bankAccountRepository.FirstOrDefaultAsync(
                        x => x.ClientId == clientId && x.TenantId == tenantId);
                    if (entity != null) { await _bankAccountRepository.DeleteAsync(entity); deletedCount++; }
                }
            }

            if (input.CategoryClientIds?.Any() == true)
            {
                foreach (var clientId in input.CategoryClientIds)
                {
                    var entity = await _categoryRepository.FirstOrDefaultAsync(
                        x => x.ClientId == clientId && x.TenantId == tenantId);
                    if (entity != null) { await _categoryRepository.DeleteAsync(entity); deletedCount++; }
                }
            }

            if (input.TransactionClientIds?.Any() == true)
            {
                foreach (var clientId in input.TransactionClientIds)
                {
                    var entity = await _transactionRepository.FirstOrDefaultAsync(
                        x => x.ClientId == clientId && x.TenantId == tenantId);
                    if (entity != null) { await _transactionRepository.DeleteAsync(entity); deletedCount++; }
                }
            }

            if (input.AssetClientIds?.Any() == true)
            {
                foreach (var clientId in input.AssetClientIds)
                {
                    var entity = await _assetRepository.FirstOrDefaultAsync(
                        x => x.ClientId == clientId && x.TenantId == tenantId);
                    if (entity != null) { await _assetRepository.DeleteAsync(entity); deletedCount++; }
                }
            }

            if (input.LiabilityClientIds?.Any() == true)
            {
                foreach (var clientId in input.LiabilityClientIds)
                {
                    var entity = await _liabilityRepository.FirstOrDefaultAsync(
                        x => x.ClientId == clientId && x.TenantId == tenantId);
                    if (entity != null) { await _liabilityRepository.DeleteAsync(entity); deletedCount++; }
                }
            }

            if (input.PartyClientIds?.Any() == true)
            {
                foreach (var clientId in input.PartyClientIds)
                {
                    var entity = await _partyRepository.FirstOrDefaultAsync(
                        x => x.ClientId == clientId && x.TenantId == tenantId);
                    if (entity != null) { await _partyRepository.DeleteAsync(entity); deletedCount++; }
                }
            }

            if (input.ExpectedIncomeClientIds?.Any() == true)
            {
                foreach (var clientId in input.ExpectedIncomeClientIds)
                {
                    var entity = await _expectedIncomeRepository.FirstOrDefaultAsync(
                        x => x.ClientId == clientId && x.TenantId == tenantId);
                    if (entity != null) { await _expectedIncomeRepository.DeleteAsync(entity); deletedCount++; }
                }
            }

            if (input.FixedExpenseClientIds?.Any() == true)
            {
                foreach (var clientId in input.FixedExpenseClientIds)
                {
                    var entity = await _fixedExpenseRepository.FirstOrDefaultAsync(
                        x => x.ClientId == clientId && x.TenantId == tenantId);
                    if (entity != null) { await _fixedExpenseRepository.DeleteAsync(entity); deletedCount++; }
                }
            }

            if (input.BudgetCategoryClientIds?.Any() == true)
            {
                foreach (var clientId in input.BudgetCategoryClientIds)
                {
                    var entity = await _budgetCategoryRepository.FirstOrDefaultAsync(
                        x => x.ClientId == clientId && x.TenantId == tenantId);
                    if (entity != null) { await _budgetCategoryRepository.DeleteAsync(entity); deletedCount++; }
                }
            }

            result.Success = true;
            result.DeletedCount = deletedCount;
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.ErrorMessage = ex.Message;
            Logger.Error("Error deleting records", ex);
        }

        return result;
    }

    public async Task<FtDeleteRecordsResult> DeleteAllData(FtDeleteAllDataInput input)
    {
        var result = new FtDeleteRecordsResult();

        if (!input.ConfirmDelete)
        {
            result.Success = false;
            result.ErrorMessage = "Please confirm deletion by setting ConfirmDelete to true.";
            return result;
        }

        try
        {
            var tenantId = AbpSession.GetTenantId();
            var deletedCount = 0;

            var transactions = await _transactionRepository.GetAll().Where(x => x.TenantId == tenantId).ToListAsync();
            foreach (var t in transactions) { await _transactionRepository.DeleteAsync(t); deletedCount++; }

            var budgets = await _budgetCategoryRepository.GetAll().Where(x => x.TenantId == tenantId).ToListAsync();
            foreach (var b in budgets) { await _budgetCategoryRepository.DeleteAsync(b); deletedCount++; }

            var fixedExp = await _fixedExpenseRepository.GetAll().Where(x => x.TenantId == tenantId).ToListAsync();
            foreach (var f in fixedExp) { await _fixedExpenseRepository.DeleteAsync(f); deletedCount++; }

            var expected = await _expectedIncomeRepository.GetAll().Where(x => x.TenantId == tenantId).ToListAsync();
            foreach (var e in expected) { await _expectedIncomeRepository.DeleteAsync(e); deletedCount++; }

            var parties = await _partyRepository.GetAll().Where(x => x.TenantId == tenantId).ToListAsync();
            foreach (var p in parties) { await _partyRepository.DeleteAsync(p); deletedCount++; }

            var liabilities = await _liabilityRepository.GetAll().Where(x => x.TenantId == tenantId).ToListAsync();
            foreach (var l in liabilities) { await _liabilityRepository.DeleteAsync(l); deletedCount++; }

            var assets = await _assetRepository.GetAll().Where(x => x.TenantId == tenantId).ToListAsync();
            foreach (var a in assets) { await _assetRepository.DeleteAsync(a); deletedCount++; }

            var categories = await _categoryRepository.GetAll().Where(x => x.TenantId == tenantId).ToListAsync();
            foreach (var c in categories) { await _categoryRepository.DeleteAsync(c); deletedCount++; }

            var accounts = await _bankAccountRepository.GetAll().Where(x => x.TenantId == tenantId).ToListAsync();
            foreach (var acc in accounts) { await _bankAccountRepository.DeleteAsync(acc); deletedCount++; }

            result.Success = true;
            result.DeletedCount = deletedCount;
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.ErrorMessage = ex.Message;
            Logger.Error("Error deleting all data", ex);
        }

        return result;
    }

    // ============ Helper Methods ============

    private FtBankAccountDto MapToBankAccountDto(FtBankAccount x) => new()
    {
        Id = x.Id,
        ClientId = x.ClientId,
        Name = x.Name,
        AccountType = x.AccountType,
        Balance = x.Balance,
        OpeningBalance = x.OpeningBalance,
        IsDefault = x.IsDefault,
        DisplayOrder = x.DisplayOrder,
        LastSyncedAt = x.LastSyncedAt,
        CreationTime = x.CreationTime,
        LastModificationTime = x.LastModificationTime
    };

    private FtCategoryDto MapToCategoryDto(FtCategory x) => new()
    {
        Id = x.Id,
        ClientId = x.ClientId,
        Name = x.Name,
        Icon = x.Icon,
        Color = x.Color,
        CategoryType = x.CategoryType,
        IsSystem = x.IsSystem,
        DisplayOrder = x.DisplayOrder,
        LastSyncedAt = x.LastSyncedAt,
        CreationTime = x.CreationTime,
        LastModificationTime = x.LastModificationTime
    };

    private FtTransactionDto MapToTransactionDto(FtTransaction x) => new()
    {
        Id = x.Id,
        ClientId = x.ClientId,
        Description = x.Description,
        Amount = x.Amount,
        TransactionType = x.TransactionType,
        PaymentMode = x.PaymentMode,
        TransactionDate = x.TransactionDate,
        Notes = x.Notes,
        LastSyncedAt = x.LastSyncedAt,
        CreationTime = x.CreationTime,
        LastModificationTime = x.LastModificationTime
    };

    private FtAssetDto MapToAssetDto(FtAsset x) => new()
    {
        Id = x.Id,
        ClientId = x.ClientId,
        Name = x.Name,
        AssetType = x.AssetType,
        TotalValue = x.TotalValue,
        PaidAmount = x.PaidAmount,
        PendingAmount = x.PendingAmount,
        PurchaseDate = x.PurchaseDate,
        Notes = x.Notes,
        LastSyncedAt = x.LastSyncedAt,
        CreationTime = x.CreationTime,
        LastModificationTime = x.LastModificationTime
    };

    private FtLiabilityDto MapToLiabilityDto(FtLiability x) => new()
    {
        Id = x.Id,
        ClientId = x.ClientId,
        Name = x.Name,
        LiabilityType = x.LiabilityType,
        Principal = x.Principal,
        PaidAmount = x.PaidAmount,
        PendingAmount = x.PendingAmount,
        InterestRate = x.InterestRate,
        Emi = x.Emi,
        EmiDueDay = x.EmiDueDay,
        StartDate = x.StartDate,
        EndDate = x.EndDate,
        Notes = x.Notes,
        LastSyncedAt = x.LastSyncedAt,
        CreationTime = x.CreationTime,
        LastModificationTime = x.LastModificationTime
    };

    private FtPartyDto MapToPartyDto(FtParty x) => new()
    {
        Id = x.Id,
        ClientId = x.ClientId,
        Name = x.Name,
        Phone = x.Phone,
        OwesYou = x.OwesYou,
        YouOwe = x.YouOwe,
        NetBalance = x.NetBalance,
        Notes = x.Notes,
        LastSyncedAt = x.LastSyncedAt,
        CreationTime = x.CreationTime,
        LastModificationTime = x.LastModificationTime
    };

    private FtExpectedIncomeDto MapToExpectedIncomeDto(FtExpectedIncome x) => new()
    {
        Id = x.Id,
        ClientId = x.ClientId,
        Source = x.Source,
        Amount = x.Amount,
        Frequency = x.Frequency,
        NextDate = x.NextDate,
        DayOfMonth = x.DayOfMonth,
        Status = x.Status,
        IsActive = x.IsActive,
        Notes = x.Notes,
        LastSyncedAt = x.LastSyncedAt,
        CreationTime = x.CreationTime,
        LastModificationTime = x.LastModificationTime
    };

    private FtFixedExpenseDto MapToFixedExpenseDto(FtFixedExpense x) => new()
    {
        Id = x.Id,
        ClientId = x.ClientId,
        Name = x.Name,
        Amount = x.Amount,
        DueDate = x.DueDate,
        Month = x.Month,
        Year = x.Year,
        Status = x.Status,
        PaidDate = x.PaidDate,
        IsRecurring = x.IsRecurring,
        Notes = x.Notes,
        LastSyncedAt = x.LastSyncedAt,
        CreationTime = x.CreationTime,
        LastModificationTime = x.LastModificationTime
    };

    private FtBudgetCategoryDto MapToBudgetCategoryDto(FtBudgetCategory x) => new()
    {
        Id = x.Id,
        ClientId = x.ClientId,
        CategoryName = x.CategoryName,
        AllocatedAmount = x.AllocatedAmount,
        SpentAmount = x.SpentAmount,
        Month = x.Month,
        Year = x.Year,
        LastSyncedAt = x.LastSyncedAt,
        CreationTime = x.CreationTime,
        LastModificationTime = x.LastModificationTime
    };
}
