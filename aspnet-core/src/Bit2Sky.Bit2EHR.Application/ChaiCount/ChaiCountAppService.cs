using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Net.Mail;
using Abp.Runtime.Session;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Bit2Sky.Bit2EHR.ChaiCount.Dto;

namespace Bit2Sky.Bit2EHR.ChaiCount;

/// <summary>
/// Application service for ChaiCount mobile app
/// Handles sync, data management, and email reports
/// </summary>
[AbpAuthorize]
public class ChaiCountAppService : Bit2EHRAppServiceBase, IChaiCountAppService
{
    private readonly IRepository<ChaiCountItem, Guid> _itemRepository;
    private readonly IRepository<ChaiCountSale, Guid> _saleRepository;
    private readonly IRepository<ChaiCountSaleItem, Guid> _saleItemRepository;
    private readonly IRepository<ChaiCountCustomer, Guid> _customerRepository;
    private readonly IRepository<ChaiCountInventory, Guid> _inventoryRepository;
    private readonly IRepository<ChaiCountShopSettings, Guid> _settingsRepository;
    private readonly IRepository<ChaiCountOffer, Guid> _offerRepository;
    private readonly IRepository<ChaiCountLoyaltyConfig, Guid> _loyaltyConfigRepository;
    private readonly IRepository<ChaiCountItemUsage, Guid> _itemUsageRepository;
    private readonly IRepository<ChaiCountStockPurchase, Guid> _stockPurchaseRepository;
    private readonly IRepository<ChaiCountCreditTransaction, Guid> _creditTransactionRepository;
    private readonly IEmailSender _emailSender;

    public ChaiCountAppService(
        IRepository<ChaiCountItem, Guid> itemRepository,
        IRepository<ChaiCountSale, Guid> saleRepository,
        IRepository<ChaiCountSaleItem, Guid> saleItemRepository,
        IRepository<ChaiCountCustomer, Guid> customerRepository,
        IRepository<ChaiCountInventory, Guid> inventoryRepository,
        IRepository<ChaiCountShopSettings, Guid> settingsRepository,
        IRepository<ChaiCountOffer, Guid> offerRepository,
        IRepository<ChaiCountLoyaltyConfig, Guid> loyaltyConfigRepository,
        IRepository<ChaiCountItemUsage, Guid> itemUsageRepository,
        IRepository<ChaiCountStockPurchase, Guid> stockPurchaseRepository,
        IRepository<ChaiCountCreditTransaction, Guid> creditTransactionRepository,
        IEmailSender emailSender)
    {
        _itemRepository = itemRepository;
        _saleRepository = saleRepository;
        _saleItemRepository = saleItemRepository;
        _customerRepository = customerRepository;
        _inventoryRepository = inventoryRepository;
        _settingsRepository = settingsRepository;
        _offerRepository = offerRepository;
        _loyaltyConfigRepository = loyaltyConfigRepository;
        _itemUsageRepository = itemUsageRepository;
        _stockPurchaseRepository = stockPurchaseRepository;
        _creditTransactionRepository = creditTransactionRepository;
        _emailSender = emailSender;
    }

    /// <summary>
    /// Sync items from mobile to server (upsert based on ClientId)
    /// </summary>
    public async Task<SyncResult> SyncItems(SyncItemsInput input)
    {
        var result = new SyncResult
        {
            SyncedRecords = new List<SyncedRecordInfo>()
        };

        try
        {
            var tenantId = AbpSession.GetTenantId();

            foreach (var itemDto in input.Items)
            {
                // Check if item exists by ClientId
                var existingItem = await _itemRepository.FirstOrDefaultAsync(
                    x => x.ClientId == itemDto.ClientId && x.TenantId == tenantId);

                if (existingItem != null)
                {
                    // Update only if client has newer data
                    if (itemDto.LastSyncedAt > existingItem.LastSyncedAt)
                    {
                        existingItem.Name = itemDto.Name;
                        existingItem.Emoji = itemDto.Emoji;
                        existingItem.Category = itemDto.Category;
                        existingItem.Price = itemDto.Price;
                        existingItem.StockQuantity = itemDto.StockQuantity;
                        existingItem.MinStockLevel = itemDto.MinStockLevel;
                        existingItem.DisplayOrder = itemDto.DisplayOrder;
                        existingItem.IsActive = itemDto.IsActive;
                        existingItem.LastSyncedAt = DateTime.UtcNow;
                        await _itemRepository.UpdateAsync(existingItem);
                    }

                    result.SyncedRecords.Add(new SyncedRecordInfo
                    {
                        ClientId = itemDto.ClientId,
                        ServerId = existingItem.Id,
                        SyncedAt = DateTime.UtcNow
                    });
                }
                else
                {
                    // Create new item
                    var newItem = new ChaiCountItem(
                        Guid.NewGuid(),
                        tenantId,
                        itemDto.ClientId,
                        itemDto.Name,
                        itemDto.Emoji,
                        itemDto.Price,
                        itemDto.Category)
                    {
                        StockQuantity = itemDto.StockQuantity,
                        MinStockLevel = itemDto.MinStockLevel,
                        DisplayOrder = itemDto.DisplayOrder,
                        IsActive = itemDto.IsActive
                    };

                    await _itemRepository.InsertAsync(newItem);

                    result.SyncedRecords.Add(new SyncedRecordInfo
                    {
                        ClientId = itemDto.ClientId,
                        ServerId = newItem.Id,
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
            Logger.Error("Error syncing items", ex);
        }

        return result;
    }

    /// <summary>
    /// Sync sales from mobile to server
    /// </summary>
    public async Task<SyncResult> SyncSales(SyncSalesInput input)
    {
        var result = new SyncResult
        {
            SyncedRecords = new List<SyncedRecordInfo>()
        };

        try
        {
            var tenantId = AbpSession.GetTenantId();

            foreach (var saleDto in input.Sales)
            {
                // Check if sale exists by ClientId
                var existingSale = await _saleRepository.FirstOrDefaultAsync(
                    x => x.ClientId == saleDto.ClientId && x.TenantId == tenantId);

                if (existingSale != null)
                {
                    // Update only if client has newer data
                    if (saleDto.LastSyncedAt > existingSale.LastSyncedAt)
                    {
                        existingSale.TotalAmount = saleDto.TotalAmount;
                        existingSale.TotalItems = saleDto.TotalItems;
                        existingSale.Notes = saleDto.Notes;
                        existingSale.IsDayClosed = saleDto.IsDayClosed;
                        existingSale.LastSyncedAt = DateTime.UtcNow;
                        await _saleRepository.UpdateAsync(existingSale);
                    }

                    result.SyncedRecords.Add(new SyncedRecordInfo
                    {
                        ClientId = saleDto.ClientId,
                        ServerId = existingSale.Id,
                        SyncedAt = DateTime.UtcNow
                    });
                }
                else
                {
                    // Create new sale
                    var newSale = new ChaiCountSale(
                        Guid.NewGuid(),
                        tenantId,
                        saleDto.ClientId,
                        saleDto.SaleDate,
                        saleDto.TotalAmount,
                        saleDto.TotalItems)
                    {
                        CustomerId = saleDto.CustomerId,
                        Notes = saleDto.Notes,
                        IsDayClosed = saleDto.IsDayClosed
                    };

                    await _saleRepository.InsertAsync(newSale);

                    // Sync sale items
                    if (saleDto.SaleItems != null)
                    {
                        foreach (var itemDto in saleDto.SaleItems)
                        {
                            // Find item by client ID
                            var item = await _itemRepository.FirstOrDefaultAsync(
                                x => x.ClientId == itemDto.ItemClientId && x.TenantId == tenantId);

                            var saleItem = new ChaiCountSaleItem(
                                Guid.NewGuid(),
                                tenantId,
                                itemDto.ClientId,
                                newSale.Id,
                                item?.Id ?? Guid.Empty,
                                itemDto.ItemName,
                                itemDto.Quantity,
                                itemDto.UnitPrice);

                            await _saleItemRepository.InsertAsync(saleItem);
                        }
                    }

                    result.SyncedRecords.Add(new SyncedRecordInfo
                    {
                        ClientId = saleDto.ClientId,
                        ServerId = newSale.Id,
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
            Logger.Error("Error syncing sales", ex);
        }

        return result;
    }

    /// <summary>
    /// Sync customers from mobile to server
    /// </summary>
    public async Task<SyncResult> SyncCustomers(SyncCustomersInput input)
    {
        var result = new SyncResult
        {
            SyncedRecords = new List<SyncedRecordInfo>()
        };

        try
        {
            var tenantId = AbpSession.GetTenantId();

            foreach (var customerDto in input.Customers)
            {
                // Check if customer exists by ClientId
                var existingCustomer = await _customerRepository.FirstOrDefaultAsync(
                    x => x.ClientId == customerDto.ClientId && x.TenantId == tenantId);

                if (existingCustomer != null)
                {
                    // Update only if client has newer data
                    if (customerDto.LastSyncedAt > existingCustomer.LastSyncedAt)
                    {
                        existingCustomer.Name = customerDto.Name;
                        existingCustomer.Phone = customerDto.Phone;
                        existingCustomer.LoyaltyPoints = customerDto.LoyaltyPoints;
                        existingCustomer.TotalVisits = customerDto.TotalVisits;
                        existingCustomer.TotalSpent = customerDto.TotalSpent;
                        existingCustomer.LastVisitDate = customerDto.LastVisitDate;
                        existingCustomer.HasLeftReview = customerDto.HasLeftReview;
                        existingCustomer.Notes = customerDto.Notes;
                        existingCustomer.Organization = customerDto.Organization;
                        existingCustomer.CreditBalance = customerDto.CreditBalance;
                        existingCustomer.IsUdhariAccount = customerDto.IsUdhariAccount;
                        existingCustomer.FreeItemsRedeemed = customerDto.FreeItemsRedeemed;
                        existingCustomer.LastSyncedAt = DateTime.UtcNow;
                        await _customerRepository.UpdateAsync(existingCustomer);
                    }

                    result.SyncedRecords.Add(new SyncedRecordInfo
                    {
                        ClientId = customerDto.ClientId,
                        ServerId = existingCustomer.Id,
                        SyncedAt = DateTime.UtcNow
                    });
                }
                else
                {
                    // Create new customer
                    var newCustomer = new ChaiCountCustomer(
                        Guid.NewGuid(),
                        tenantId,
                        customerDto.ClientId,
                        customerDto.Name,
                        customerDto.Phone)
                    {
                        LoyaltyPoints = customerDto.LoyaltyPoints,
                        TotalVisits = customerDto.TotalVisits,
                        TotalSpent = customerDto.TotalSpent,
                        LastVisitDate = customerDto.LastVisitDate,
                        HasLeftReview = customerDto.HasLeftReview,
                        Notes = customerDto.Notes,
                        Organization = customerDto.Organization,
                        CreditBalance = customerDto.CreditBalance,
                        IsUdhariAccount = customerDto.IsUdhariAccount,
                        FreeItemsRedeemed = customerDto.FreeItemsRedeemed
                    };

                    await _customerRepository.InsertAsync(newCustomer);

                    result.SyncedRecords.Add(new SyncedRecordInfo
                    {
                        ClientId = customerDto.ClientId,
                        ServerId = newCustomer.Id,
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
            Logger.Error("Error syncing customers", ex);
        }

        return result;
    }

    /// <summary>
    /// Pull all data from server for mobile sync
    /// </summary>
    public async Task<PullDataOutput> PullData(DateTime? lastSync)
    {
        var tenantId = AbpSession.GetTenantId();

        var itemsQuery = _itemRepository.GetAll()
            .Where(x => x.TenantId == tenantId && !x.IsDeleted);

        var salesQuery = _saleRepository.GetAll()
            .Include(s => s.SaleItems)
            .Where(x => x.TenantId == tenantId && !x.IsDeleted);

        var customersQuery = _customerRepository.GetAll()
            .Where(x => x.TenantId == tenantId && !x.IsDeleted);

        var inventoryQuery = _inventoryRepository.GetAll()
            .Where(x => x.TenantId == tenantId && !x.IsDeleted);

        var creditTransactionsQuery = _creditTransactionRepository.GetAll()
            .Include(t => t.Customer)
            .Where(x => x.TenantId == tenantId && !x.IsDeleted);

        // Filter by last sync time if provided
        if (lastSync.HasValue)
        {
            itemsQuery = itemsQuery.Where(x => x.LastSyncedAt > lastSync.Value || x.LastModificationTime > lastSync.Value);
            salesQuery = salesQuery.Where(x => x.LastSyncedAt > lastSync.Value || x.LastModificationTime > lastSync.Value);
            customersQuery = customersQuery.Where(x => x.LastSyncedAt > lastSync.Value || x.LastModificationTime > lastSync.Value);
            inventoryQuery = inventoryQuery.Where(x => x.LastSyncedAt > lastSync.Value || x.LastModificationTime > lastSync.Value);
            creditTransactionsQuery = creditTransactionsQuery.Where(x => x.LastSyncedAt > lastSync.Value || x.LastModificationTime > lastSync.Value);
        }

        var items = await itemsQuery.ToListAsync();
        var sales = await salesQuery.ToListAsync();
        var customers = await customersQuery.ToListAsync();
        var inventory = await inventoryQuery.ToListAsync();
        var creditTransactions = await creditTransactionsQuery.ToListAsync();

        return new PullDataOutput
        {
            Items = items.Select(x => new ChaiCountItemDto
            {
                Id = x.Id,
                ClientId = x.ClientId,
                Name = x.Name,
                Emoji = x.Emoji,
                Category = x.Category,
                Price = x.Price,
                StockQuantity = x.StockQuantity,
                MinStockLevel = x.MinStockLevel,
                DisplayOrder = x.DisplayOrder,
                IsActive = x.IsActive,
                LastSyncedAt = x.LastSyncedAt,
                CreationTime = x.CreationTime,
                LastModificationTime = x.LastModificationTime
            }).ToList(),
            // Aggregate sales by date for mobile app
            // Calculate totals from SaleItems (more accurate than Sale.TotalAmount which may be stale)
            Sales = sales
                .GroupBy(s => s.SaleDate.Date)
                .Select(g => {
                    // Get all sale items for this day
                    var allItems = g.SelectMany(s => s.SaleItems ?? new List<ChaiCountSaleItem>()).ToList();
                    // Aggregate items by name
                    var aggregatedItems = allItems
                        .GroupBy(si => si.ItemName)
                        .Select(sig => new ChaiCountSaleItemDto
                        {
                            Id = sig.First().Id,
                            ClientId = sig.First().ClientId,
                            SaleId = sig.First().SaleId,
                            ItemId = sig.First().ItemId,
                            ItemName = sig.Key,
                            Quantity = sig.Sum(si => si.Quantity),
                            UnitPrice = sig.First().UnitPrice,
                            TotalAmount = sig.Sum(si => si.TotalAmount)
                        }).ToList();

                    // Calculate totals from actual items (not from possibly stale Sale record)
                    var totalFromItems = allItems.Sum(si => si.TotalAmount);
                    var itemCountFromItems = allItems.Sum(si => si.Quantity);

                    return new ChaiCountSaleDto
                    {
                        Id = g.First().Id,
                        ClientId = g.Key.ToString("yyyy-MM-dd"),
                        SaleDate = g.Key,
                        TotalAmount = totalFromItems,  // Use calculated total from items
                        TotalItems = itemCountFromItems,  // Use calculated count from items
                        CustomerId = null,
                        Notes = $"Aggregated from {g.Count()} sales, {allItems.Count} items",
                        IsDayClosed = g.Any(s => s.IsDayClosed),
                        LastSyncedAt = g.Max(s => s.LastSyncedAt),
                        CreationTime = g.Min(s => s.CreationTime),
                        SaleItems = aggregatedItems
                    };
                }).ToList(),
            Customers = customers.Select(c => new ChaiCountCustomerDto
            {
                Id = c.Id,
                ClientId = c.ClientId,
                Name = c.Name,
                Phone = c.Phone,
                LoyaltyPoints = c.LoyaltyPoints,
                TotalVisits = c.TotalVisits,
                TotalSpent = c.TotalSpent,
                LastVisitDate = c.LastVisitDate,
                HasLeftReview = c.HasLeftReview,
                Notes = c.Notes,
                Organization = c.Organization,
                CreditBalance = c.CreditBalance,
                IsUdhariAccount = c.IsUdhariAccount,
                FreeItemsRedeemed = c.FreeItemsRedeemed,
                LastSyncedAt = c.LastSyncedAt,
                CreationTime = c.CreationTime,
                LastModificationTime = c.LastModificationTime
            }).ToList(),
            Inventory = inventory.Select(i => new ChaiCountInventoryDto
            {
                Id = i.Id,
                ClientId = i.ClientId,
                Name = i.Name,
                Emoji = i.Emoji,
                Quantity = i.Quantity,
                Unit = i.Unit,
                LowStockThreshold = i.LowStockThreshold,
                CostPerUnit = i.CostPerUnit,
                IsLowStock = i.IsLowStock,
                LastSyncedAt = i.LastSyncedAt,
                CreationTime = i.CreationTime,
                LastModificationTime = i.LastModificationTime
            }).ToList(),
            CreditTransactions = creditTransactions.Select(t => new ChaiCountCreditTransactionDto
            {
                Id = t.Id,
                ClientId = t.ClientId,
                CustomerId = t.CustomerId,
                CustomerClientId = t.Customer?.ClientId,
                CustomerName = t.Customer?.Name,
                TransactionDate = t.TransactionDate,
                TransactionType = t.TransactionType,
                Amount = t.Amount,
                BalanceAfter = t.BalanceAfter,
                ItemsSummary = t.ItemsSummary,
                Notes = t.Notes,
                LastSyncedAt = t.LastSyncedAt,
                CreationTime = t.CreationTime,
                LastModificationTime = t.LastModificationTime
            }).ToList(),
            ServerTime = DateTime.UtcNow
        };
    }

    /// <summary>
    /// Sync inventory from mobile to server
    /// </summary>
    public async Task<SyncResult> SyncInventory(SyncInventoryInput input)
    {
        var result = new SyncResult
        {
            SyncedRecords = new List<SyncedRecordInfo>()
        };

        try
        {
            var tenantId = AbpSession.GetTenantId();

            foreach (var inventoryDto in input.Items)
            {
                var existingItem = await _inventoryRepository.FirstOrDefaultAsync(
                    x => x.ClientId == inventoryDto.ClientId && x.TenantId == tenantId);

                if (existingItem != null)
                {
                    if (inventoryDto.LastSyncedAt > existingItem.LastSyncedAt)
                    {
                        existingItem.Name = inventoryDto.Name;
                        existingItem.Emoji = inventoryDto.Emoji;
                        existingItem.Quantity = inventoryDto.Quantity;
                        existingItem.Unit = inventoryDto.Unit;
                        existingItem.LowStockThreshold = inventoryDto.LowStockThreshold;
                        existingItem.CostPerUnit = inventoryDto.CostPerUnit;
                        existingItem.LastSyncedAt = DateTime.UtcNow;
                        await _inventoryRepository.UpdateAsync(existingItem);
                    }

                    result.SyncedRecords.Add(new SyncedRecordInfo
                    {
                        ClientId = inventoryDto.ClientId,
                        ServerId = existingItem.Id,
                        SyncedAt = DateTime.UtcNow
                    });
                }
                else
                {
                    var newItem = new ChaiCountInventory(
                        Guid.NewGuid(),
                        tenantId,
                        inventoryDto.ClientId,
                        inventoryDto.Name,
                        inventoryDto.Emoji,
                        inventoryDto.Unit,
                        inventoryDto.Quantity,
                        inventoryDto.LowStockThreshold,
                        inventoryDto.CostPerUnit);

                    await _inventoryRepository.InsertAsync(newItem);

                    result.SyncedRecords.Add(new SyncedRecordInfo
                    {
                        ClientId = inventoryDto.ClientId,
                        ServerId = newItem.Id,
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
            Logger.Error("Error syncing inventory", ex);
        }

        return result;
    }

    // ============ Credit Transaction (Udhari) APIs ============

    /// <summary>
    /// Sync credit transactions from mobile to server
    /// </summary>
    public async Task<SyncResult> SyncCreditTransactions(SyncCreditTransactionsInput input)
    {
        var result = new SyncResult { SyncedRecords = new List<SyncedRecordInfo>() };

        try
        {
            var tenantId = AbpSession.GetTenantId();

            foreach (var transactionDto in input.CreditTransactions)
            {
                // Find customer by ClientId
                var customer = await _customerRepository.FirstOrDefaultAsync(
                    x => x.ClientId == transactionDto.CustomerClientId && x.TenantId == tenantId);

                if (customer == null)
                {
                    Logger.Warn($"Customer with ClientId {transactionDto.CustomerClientId} not found for credit transaction");
                    continue;
                }

                var existingTransaction = await _creditTransactionRepository.FirstOrDefaultAsync(
                    x => x.ClientId == transactionDto.ClientId && x.TenantId == tenantId);

                if (existingTransaction != null)
                {
                    // Update if client has newer data
                    if (transactionDto.LastSyncedAt > existingTransaction.LastSyncedAt)
                    {
                        existingTransaction.CustomerId = customer.Id;
                        existingTransaction.TransactionDate = transactionDto.TransactionDate;
                        existingTransaction.TransactionType = transactionDto.TransactionType;
                        existingTransaction.Amount = transactionDto.Amount;
                        existingTransaction.BalanceAfter = transactionDto.BalanceAfter;
                        existingTransaction.ItemsSummary = transactionDto.ItemsSummary;
                        existingTransaction.Notes = transactionDto.Notes;
                        existingTransaction.LastSyncedAt = DateTime.UtcNow;
                        await _creditTransactionRepository.UpdateAsync(existingTransaction);
                    }

                    result.SyncedRecords.Add(new SyncedRecordInfo
                    {
                        ClientId = transactionDto.ClientId,
                        ServerId = existingTransaction.Id,
                        SyncedAt = DateTime.UtcNow
                    });
                }
                else
                {
                    var newTransaction = new ChaiCountCreditTransaction(
                        Guid.NewGuid(),
                        tenantId,
                        transactionDto.ClientId,
                        customer.Id,
                        transactionDto.TransactionDate,
                        transactionDto.TransactionType,
                        transactionDto.Amount,
                        transactionDto.BalanceAfter)
                    {
                        ItemsSummary = transactionDto.ItemsSummary,
                        Notes = transactionDto.Notes
                    };

                    await _creditTransactionRepository.InsertAsync(newTransaction);

                    result.SyncedRecords.Add(new SyncedRecordInfo
                    {
                        ClientId = transactionDto.ClientId,
                        ServerId = newTransaction.Id,
                        SyncedAt = DateTime.UtcNow
                    });
                }

                // Update customer's credit balance to match latest
                customer.CreditBalance = transactionDto.BalanceAfter;
                customer.IsUdhariAccount = true;
                await _customerRepository.UpdateAsync(customer);
            }

            result.Success = true;
            result.SyncedCount = result.SyncedRecords.Count;
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.ErrorMessage = ex.Message;
            Logger.Error("Error syncing credit transactions", ex);
        }

        return result;
    }

    /// <summary>
    /// Get credit transactions for a customer
    /// </summary>
    public async Task<List<ChaiCountCreditTransactionDto>> GetCreditTransactions(Guid? customerId, DateTime? fromDate, DateTime? toDate)
    {
        var tenantId = AbpSession.GetTenantId();

        var query = _creditTransactionRepository.GetAll()
            .Include(t => t.Customer)
            .Where(x => x.TenantId == tenantId && !x.IsDeleted);

        if (customerId.HasValue)
            query = query.Where(x => x.CustomerId == customerId.Value);
        if (fromDate.HasValue)
            query = query.Where(x => x.TransactionDate >= fromDate.Value);
        if (toDate.HasValue)
            query = query.Where(x => x.TransactionDate <= toDate.Value);

        var transactions = await query.OrderByDescending(x => x.TransactionDate).ToListAsync();

        return transactions.Select(t => new ChaiCountCreditTransactionDto
        {
            Id = t.Id,
            ClientId = t.ClientId,
            CustomerId = t.CustomerId,
            CustomerClientId = t.Customer?.ClientId,
            CustomerName = t.Customer?.Name,
            TransactionDate = t.TransactionDate,
            TransactionType = t.TransactionType,
            Amount = t.Amount,
            BalanceAfter = t.BalanceAfter,
            ItemsSummary = t.ItemsSummary,
            Notes = t.Notes,
            LastSyncedAt = t.LastSyncedAt,
            CreationTime = t.CreationTime,
            LastModificationTime = t.LastModificationTime
        }).ToList();
    }

    /// <summary>
    /// Get Udhari summary (total outstanding credit)
    /// </summary>
    public async Task<UdhariSummaryDto> GetUdhariSummary()
    {
        var tenantId = AbpSession.GetTenantId();

        // Get all Udhari customers
        var udhariCustomers = await _customerRepository.GetAll()
            .Where(x => x.TenantId == tenantId && !x.IsDeleted && x.IsUdhariAccount)
            .ToListAsync();

        // Get all credit transactions
        var allTransactions = await _creditTransactionRepository.GetAll()
            .Where(x => x.TenantId == tenantId && !x.IsDeleted)
            .ToListAsync();

        var totalCreditGiven = allTransactions.Where(t => t.TransactionType == "credit").Sum(t => t.Amount);
        var totalPaymentsReceived = allTransactions.Where(t => t.TransactionType == "payment").Sum(t => t.Amount);

        // Get top debtors (customers with highest outstanding balance)
        var topDebtors = udhariCustomers
            .Where(c => c.CreditBalance > 0)
            .OrderByDescending(c => c.CreditBalance)
            .Take(10)
            .Select(c => new UdhariCustomerSummaryDto
            {
                CustomerId = c.Id,
                CustomerClientId = c.ClientId,
                CustomerName = c.Name,
                Organization = c.Organization,
                Phone = c.Phone,
                CreditBalance = c.CreditBalance,
                LastTransactionDate = allTransactions
                    .Where(t => t.CustomerId == c.Id)
                    .OrderByDescending(t => t.TransactionDate)
                    .FirstOrDefault()?.TransactionDate
            })
            .ToList();

        return new UdhariSummaryDto
        {
            TotalOutstanding = udhariCustomers.Sum(c => c.CreditBalance),
            TotalUdhariCustomers = udhariCustomers.Count(c => c.CreditBalance > 0),
            TotalCreditGiven = totalCreditGiven,
            TotalPaymentsReceived = totalPaymentsReceived,
            TopDebtors = topDebtors
        };
    }

    /// <summary>
    /// Get low stock alerts
    /// </summary>
    public async Task<LowStockAlertDto> GetLowStockAlerts()
    {
        var tenantId = AbpSession.GetTenantId();

        var lowStockItems = await _inventoryRepository.GetAll()
            .Where(x => x.TenantId == tenantId && !x.IsDeleted && x.Quantity <= x.LowStockThreshold)
            .ToListAsync();

        return new LowStockAlertDto
        {
            LowStockItems = lowStockItems.Select(i => new ChaiCountInventoryDto
            {
                Id = i.Id,
                ClientId = i.ClientId,
                Name = i.Name,
                Emoji = i.Emoji,
                Quantity = i.Quantity,
                Unit = i.Unit,
                LowStockThreshold = i.LowStockThreshold,
                CostPerUnit = i.CostPerUnit,
                IsLowStock = true,
                LastSyncedAt = i.LastSyncedAt,
                CreationTime = i.CreationTime,
                LastModificationTime = i.LastModificationTime
            }).ToList(),
            TotalLowStockCount = lowStockItems.Count
        };
    }

    /// <summary>
    /// Send daily report email
    /// </summary>
    public async Task SendDailyReport(SendDailyReportInput input)
    {
        var user = await GetCurrentUserAsync();

        if (string.IsNullOrEmpty(user.EmailAddress))
        {
            throw new UserFriendlyException("No email address configured for your account.");
        }

        var reportHtml = GenerateReportHtml(input);

        await _emailSender.SendAsync(
            to: user.EmailAddress,
            subject: $"ChaiCount Daily Report - {input.Date:dd MMM yyyy}",
            body: reportHtml,
            isBodyHtml: true
        );
    }

    private string GenerateReportHtml(SendDailyReportInput input)
    {
        var data = input.ReportData;
        var topSellersHtml = "";

        if (data.TopSellers != null && data.TopSellers.Any())
        {
            topSellersHtml = string.Join("", data.TopSellers.Take(5).Select(ts =>
                $"<tr><td>{ts.Emoji} {ts.ItemName}</td><td style='text-align:center'>{ts.Quantity}</td><td style='text-align:right'>₹{ts.Revenue:N0}</td></tr>"));
        }

        return $@"
<!DOCTYPE html>
<html>
<head>
    <style>
        body {{ font-family: 'Segoe UI', Arial, sans-serif; margin: 0; padding: 20px; background: #f5f5f5; }}
        .container {{ max-width: 600px; margin: 0 auto; background: white; border-radius: 12px; overflow: hidden; box-shadow: 0 2px 10px rgba(0,0,0,0.1); }}
        .header {{ background: linear-gradient(135deg, #D97706, #EA580C); color: white; padding: 30px; text-align: center; }}
        .header h1 {{ margin: 0; font-size: 24px; }}
        .header p {{ margin: 10px 0 0; opacity: 0.9; }}
        .content {{ padding: 30px; }}
        .stats {{ display: flex; justify-content: space-around; margin-bottom: 30px; text-align: center; }}
        .stat {{ padding: 15px; }}
        .stat-value {{ font-size: 28px; font-weight: bold; color: #D97706; }}
        .stat-label {{ font-size: 12px; color: #666; text-transform: uppercase; }}
        .section {{ margin-bottom: 25px; }}
        .section-title {{ font-size: 14px; color: #888; text-transform: uppercase; margin-bottom: 10px; }}
        table {{ width: 100%; border-collapse: collapse; }}
        th, td {{ padding: 12px; border-bottom: 1px solid #eee; }}
        th {{ text-align: left; font-size: 12px; color: #888; text-transform: uppercase; }}
        .footer {{ background: #f9f9f9; padding: 20px; text-align: center; font-size: 12px; color: #888; }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>{data.ShopName}</h1>
            <p>Daily Sales Report - {input.Date:dd MMMM yyyy}</p>
        </div>
        <div class='content'>
            <div class='stats'>
                <div class='stat'>
                    <div class='stat-value'>₹{data.TotalRevenue:N0}</div>
                    <div class='stat-label'>Total Revenue</div>
                </div>
                <div class='stat'>
                    <div class='stat-value'>{data.TotalItems}</div>
                    <div class='stat-label'>Items Sold</div>
                </div>
                <div class='stat'>
                    <div class='stat-value'>{data.TotalTransactions}</div>
                    <div class='stat-label'>Transactions</div>
                </div>
            </div>

            <div class='section'>
                <div class='section-title'>Top Sellers</div>
                <table>
                    <tr>
                        <th>Item</th>
                        <th style='text-align:center'>Qty</th>
                        <th style='text-align:right'>Revenue</th>
                    </tr>
                    {topSellersHtml}
                </table>
            </div>
        </div>
        <div class='footer'>
            <p>Generated by ChaiCount - Smart Inventory for Tea Shops</p>
        </div>
    </div>
</body>
</html>";
    }

    // ============ Settings & Configuration APIs ============

    /// <summary>
    /// Sync shop settings from mobile to server
    /// </summary>
    public async Task<ChaiCountShopSettingsDto> SyncSettings(SyncShopSettingsInput input)
    {
        var tenantId = AbpSession.GetTenantId();

        var existingSettings = await _settingsRepository.FirstOrDefaultAsync(
            x => x.TenantId == tenantId);

        if (existingSettings != null)
        {
            existingSettings.ShopName = input.ShopName;
            existingSettings.OwnerName = input.OwnerName;
            existingSettings.PhoneNumber = input.PhoneNumber;
            existingSettings.Address = input.Address;
            existingSettings.UpiId = input.UpiId;
            existingSettings.GoogleReviewLink = input.GoogleReviewLink;
            existingSettings.ReportEmail = input.ReportEmail;
            existingSettings.CurrencySymbol = input.CurrencySymbol ?? "₹";
            existingSettings.Timezone = input.Timezone ?? "Asia/Kolkata";
            existingSettings.LastSyncedAt = DateTime.UtcNow;
            await _settingsRepository.UpdateAsync(existingSettings);
        }
        else
        {
            existingSettings = new ChaiCountShopSettings
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                ClientId = input.ClientId ?? "settings",
                ShopName = input.ShopName ?? "My Tea Shop",
                OwnerName = input.OwnerName,
                PhoneNumber = input.PhoneNumber,
                Address = input.Address,
                UpiId = input.UpiId,
                GoogleReviewLink = input.GoogleReviewLink,
                ReportEmail = input.ReportEmail,
                CurrencySymbol = input.CurrencySymbol ?? "₹",
                Timezone = input.Timezone ?? "Asia/Kolkata",
                LastSyncedAt = DateTime.UtcNow
            };
            await _settingsRepository.InsertAsync(existingSettings);
        }

        return MapToSettingsDto(existingSettings);
    }

    /// <summary>
    /// Get shop settings
    /// </summary>
    public async Task<ChaiCountShopSettingsDto> GetSettings()
    {
        var tenantId = AbpSession.GetTenantId();
        var settings = await _settingsRepository.FirstOrDefaultAsync(x => x.TenantId == tenantId);

        if (settings == null)
        {
            return new ChaiCountShopSettingsDto
            {
                ShopName = "My Tea Shop",
                CurrencySymbol = "₹",
                Timezone = "Asia/Kolkata"
            };
        }

        return MapToSettingsDto(settings);
    }

    /// <summary>
    /// Sync loyalty configuration
    /// </summary>
    public async Task<ChaiCountLoyaltyConfigDto> SyncLoyaltyConfig(SyncLoyaltyConfigInput input)
    {
        var tenantId = AbpSession.GetTenantId();

        var existingConfig = await _loyaltyConfigRepository.FirstOrDefaultAsync(
            x => x.TenantId == tenantId);

        if (existingConfig != null)
        {
            existingConfig.IsEnabled = input.IsEnabled;
            existingConfig.PointsPerItem = input.PointsPerItem;
            existingConfig.PointsPerRupee = input.PointsPerRupee;
            existingConfig.RewardThreshold = input.RewardThreshold;
            existingConfig.RewardType = input.RewardType;
            existingConfig.RewardValue = input.RewardValue;
            existingConfig.RewardDescription = input.RewardDescription;
            existingConfig.PointsExpiryDays = input.PointsExpiryDays;
            existingConfig.LastSyncedAt = DateTime.UtcNow;
            await _loyaltyConfigRepository.UpdateAsync(existingConfig);
        }
        else
        {
            existingConfig = new ChaiCountLoyaltyConfig
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                ClientId = input.ClientId ?? "loyalty_config",
                IsEnabled = input.IsEnabled,
                PointsPerItem = input.PointsPerItem,
                PointsPerRupee = input.PointsPerRupee,
                RewardThreshold = input.RewardThreshold,
                RewardType = input.RewardType ?? "free_item",
                RewardValue = input.RewardValue,
                RewardDescription = input.RewardDescription,
                PointsExpiryDays = input.PointsExpiryDays,
                LastSyncedAt = DateTime.UtcNow
            };
            await _loyaltyConfigRepository.InsertAsync(existingConfig);
        }

        return MapToLoyaltyConfigDto(existingConfig);
    }

    /// <summary>
    /// Get loyalty configuration
    /// </summary>
    public async Task<ChaiCountLoyaltyConfigDto> GetLoyaltyConfig()
    {
        var tenantId = AbpSession.GetTenantId();
        var config = await _loyaltyConfigRepository.FirstOrDefaultAsync(x => x.TenantId == tenantId);

        if (config == null)
        {
            return new ChaiCountLoyaltyConfigDto
            {
                IsEnabled = true,
                PointsPerItem = 1,
                RewardThreshold = 10,
                RewardType = "free_item",
                RewardValue = 1
            };
        }

        return MapToLoyaltyConfigDto(config);
    }

    // ============ Offers APIs ============

    /// <summary>
    /// Sync offers from mobile to server
    /// </summary>
    public async Task<SyncResult> SyncOffers(SyncOffersInput input)
    {
        var result = new SyncResult { SyncedRecords = new List<SyncedRecordInfo>() };

        try
        {
            var tenantId = AbpSession.GetTenantId();

            foreach (var offerDto in input.Offers)
            {
                var existingOffer = await _offerRepository.FirstOrDefaultAsync(
                    x => x.ClientId == offerDto.ClientId && x.TenantId == tenantId);

                if (existingOffer != null)
                {
                    existingOffer.Title = offerDto.Title;
                    existingOffer.Description = offerDto.Description;
                    existingOffer.Code = offerDto.Code;
                    existingOffer.DiscountType = offerDto.DiscountType;
                    existingOffer.DiscountValue = offerDto.DiscountValue;
                    existingOffer.MinimumOrderAmount = offerDto.MinimumOrderAmount;
                    existingOffer.ValidFrom = offerDto.ValidFrom;
                    existingOffer.ValidUntil = offerDto.ValidUntil;
                    existingOffer.IsActive = offerDto.IsActive;
                    existingOffer.MaxUsageLimit = offerDto.MaxUsageLimit;
                    existingOffer.LastSyncedAt = DateTime.UtcNow;
                    await _offerRepository.UpdateAsync(existingOffer);

                    result.SyncedRecords.Add(new SyncedRecordInfo
                    {
                        ClientId = offerDto.ClientId,
                        ServerId = existingOffer.Id,
                        SyncedAt = DateTime.UtcNow
                    });
                }
                else
                {
                    var newOffer = new ChaiCountOffer
                    {
                        Id = Guid.NewGuid(),
                        TenantId = tenantId,
                        ClientId = offerDto.ClientId,
                        Title = offerDto.Title,
                        Description = offerDto.Description,
                        Code = offerDto.Code,
                        DiscountType = offerDto.DiscountType,
                        DiscountValue = offerDto.DiscountValue,
                        MinimumOrderAmount = offerDto.MinimumOrderAmount,
                        ValidFrom = offerDto.ValidFrom,
                        ValidUntil = offerDto.ValidUntil,
                        IsActive = offerDto.IsActive,
                        MaxUsageLimit = offerDto.MaxUsageLimit,
                        LastSyncedAt = DateTime.UtcNow
                    };
                    await _offerRepository.InsertAsync(newOffer);

                    result.SyncedRecords.Add(new SyncedRecordInfo
                    {
                        ClientId = offerDto.ClientId,
                        ServerId = newOffer.Id,
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
            Logger.Error("Error syncing offers", ex);
        }

        return result;
    }

    /// <summary>
    /// Get all offers
    /// </summary>
    public async Task<List<ChaiCountOfferDto>> GetOffers(bool? activeOnly)
    {
        var tenantId = AbpSession.GetTenantId();
        var query = _offerRepository.GetAll()
            .Where(x => x.TenantId == tenantId && !x.IsDeleted);

        if (activeOnly == true)
        {
            query = query.Where(x => x.IsActive);
        }

        var offers = await query.ToListAsync();
        return offers.Select(MapToOfferDto).ToList();
    }

    // ============ Item Usage APIs ============

    /// <summary>
    /// Sync item usage mappings
    /// </summary>
    public async Task<SyncResult> SyncItemUsage(SyncItemUsageInput input)
    {
        var result = new SyncResult { SyncedRecords = new List<SyncedRecordInfo>() };

        try
        {
            var tenantId = AbpSession.GetTenantId();

            foreach (var usageDto in input.Mappings)
            {
                var existingUsage = await _itemUsageRepository.FirstOrDefaultAsync(
                    x => x.ClientId == usageDto.ClientId && x.TenantId == tenantId);

                if (existingUsage != null)
                {
                    existingUsage.MenuItemClientId = usageDto.MenuItemClientId;
                    existingUsage.InventoryItemClientId = usageDto.InventoryItemClientId;
                    existingUsage.QuantityPerSale = usageDto.QuantityPerSale;
                    existingUsage.LastSyncedAt = DateTime.UtcNow;
                    await _itemUsageRepository.UpdateAsync(existingUsage);

                    result.SyncedRecords.Add(new SyncedRecordInfo
                    {
                        ClientId = usageDto.ClientId,
                        ServerId = existingUsage.Id,
                        SyncedAt = DateTime.UtcNow
                    });
                }
                else
                {
                    var newUsage = new ChaiCountItemUsage
                    {
                        Id = Guid.NewGuid(),
                        TenantId = tenantId,
                        ClientId = usageDto.ClientId,
                        MenuItemClientId = usageDto.MenuItemClientId,
                        InventoryItemClientId = usageDto.InventoryItemClientId,
                        QuantityPerSale = usageDto.QuantityPerSale,
                        LastSyncedAt = DateTime.UtcNow
                    };
                    await _itemUsageRepository.InsertAsync(newUsage);

                    result.SyncedRecords.Add(new SyncedRecordInfo
                    {
                        ClientId = usageDto.ClientId,
                        ServerId = newUsage.Id,
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
            Logger.Error("Error syncing item usage", ex);
        }

        return result;
    }

    /// <summary>
    /// Get all item usage mappings
    /// </summary>
    public async Task<List<ChaiCountItemUsageDto>> GetItemUsage()
    {
        var tenantId = AbpSession.GetTenantId();
        var usages = await _itemUsageRepository.GetAll()
            .Where(x => x.TenantId == tenantId && !x.IsDeleted)
            .ToListAsync();

        return usages.Select(u => new ChaiCountItemUsageDto
        {
            Id = u.Id,
            ClientId = u.ClientId,
            MenuItemClientId = u.MenuItemClientId,
            InventoryItemClientId = u.InventoryItemClientId,
            QuantityPerSale = u.QuantityPerSale,
            LastSyncedAt = u.LastSyncedAt
        }).ToList();
    }

    // ============ Dashboard & Analytics APIs ============

    /// <summary>
    /// Get dashboard analytics data
    /// </summary>
    public async Task<ChaiCountDashboardDto> GetDashboard(GetDashboardInput input)
    {
        var tenantId = AbpSession.GetTenantId();
        var today = DateTime.UtcNow.Date;
        var weekStart = today.AddDays(-(int)today.DayOfWeek);
        var monthStart = new DateTime(today.Year, today.Month, 1);

        // Get all sales
        var allSales = await _saleRepository.GetAll()
            .Include(s => s.SaleItems)
            .Where(x => x.TenantId == tenantId && !x.IsDeleted)
            .ToListAsync();

        var todaySales = allSales.Where(s => s.SaleDate.Date == today).ToList();
        var weekSales = allSales.Where(s => s.SaleDate.Date >= weekStart).ToList();
        var monthSales = allSales.Where(s => s.SaleDate.Date >= monthStart).ToList();

        // Get customers
        var customers = await _customerRepository.GetAll()
            .Where(x => x.TenantId == tenantId && !x.IsDeleted)
            .ToListAsync();

        // Get inventory
        var inventory = await _inventoryRepository.GetAll()
            .Where(x => x.TenantId == tenantId && !x.IsDeleted)
            .ToListAsync();

        // Calculate top sellers
        var allSaleItems = allSales.SelectMany(s => s.SaleItems ?? new List<ChaiCountSaleItem>()).ToList();
        var topSellers = allSaleItems
            .GroupBy(si => si.ItemName)
            .Select(g => new TopSellerDto
            {
                ItemName = g.Key,
                TotalQuantity = g.Sum(si => si.Quantity),
                TotalRevenue = g.Sum(si => si.TotalAmount)
            })
            .OrderByDescending(t => t.TotalRevenue)
            .Take(5)
            .ToList();

        var totalRevenue = allSales.Sum(s => s.TotalAmount);
        if (totalRevenue > 0)
        {
            foreach (var seller in topSellers)
            {
                seller.Percentage = (seller.TotalRevenue / totalRevenue) * 100;
            }
        }

        // Calculate daily revenue (last 7 days)
        var dailyRevenue = Enumerable.Range(0, 7)
            .Select(i => today.AddDays(-i))
            .Select(date => new DailyRevenueDto
            {
                Date = date.ToString("yyyy-MM-dd"),
                Revenue = allSales.Where(s => s.SaleDate.Date == date).Sum(s => s.TotalAmount),
                ItemsSold = allSales.Where(s => s.SaleDate.Date == date).Sum(s => s.TotalItems),
                Transactions = allSales.Count(s => s.SaleDate.Date == date)
            })
            .Reverse()
            .ToList();

        // Top customers
        var topCustomers = customers
            .OrderByDescending(c => c.TotalSpent)
            .Take(5)
            .Select(c => new TopCustomerDto
            {
                Name = c.Name,
                Phone = c.Phone,
                TotalVisits = c.TotalVisits,
                TotalSpent = c.TotalSpent,
                LoyaltyPoints = c.LoyaltyPoints
            })
            .ToList();

        // Low stock items
        var lowStockItems = inventory.Where(i => i.Quantity <= i.LowStockThreshold).ToList();

        // Calculate growth
        var lastWeekSales = allSales.Where(s => s.SaleDate.Date >= weekStart.AddDays(-7) && s.SaleDate.Date < weekStart).ToList();
        var revenueGrowth = lastWeekSales.Sum(s => s.TotalAmount) > 0
            ? ((weekSales.Sum(s => s.TotalAmount) - lastWeekSales.Sum(s => s.TotalAmount)) / lastWeekSales.Sum(s => s.TotalAmount)) * 100
            : 0;

        return new ChaiCountDashboardDto
        {
            TodayRevenue = todaySales.Sum(s => s.TotalAmount),
            TodayItemsSold = todaySales.Sum(s => s.TotalItems),
            TodayTransactions = todaySales.Count,
            TodayNewCustomers = customers.Count(c => c.CreationTime.Date == today),
            WeekRevenue = weekSales.Sum(s => s.TotalAmount),
            WeekItemsSold = weekSales.Sum(s => s.TotalItems),
            MonthRevenue = monthSales.Sum(s => s.TotalAmount),
            MonthItemsSold = monthSales.Sum(s => s.TotalItems),
            TotalRevenue = totalRevenue,
            TotalItemsSold = allSales.Sum(s => s.TotalItems),
            TotalCustomers = customers.Count,
            TopSellers = topSellers,
            DailyRevenue = dailyRevenue,
            LowStockCount = lowStockItems.Count,
            LowStockItems = lowStockItems.Select(i => new ChaiCountInventoryDto
            {
                Id = i.Id,
                ClientId = i.ClientId,
                Name = i.Name,
                Emoji = i.Emoji,
                Quantity = i.Quantity,
                Unit = i.Unit,
                LowStockThreshold = i.LowStockThreshold,
                CostPerUnit = i.CostPerUnit,
                IsLowStock = true
            }).ToList(),
            TopCustomers = topCustomers,
            RevenueGrowthPercent = revenueGrowth,
            GeneratedAt = DateTime.UtcNow
        };
    }

    // ============ Stock Purchase (Expenses) APIs ============

    /// <summary>
    /// Sync stock purchases from mobile to server
    /// </summary>
    public async Task<SyncResult> SyncStockPurchases(SyncStockPurchasesInput input)
    {
        var result = new SyncResult { SyncedRecords = new List<SyncedRecordInfo>() };

        try
        {
            var tenantId = AbpSession.GetTenantId();

            foreach (var purchaseDto in input.Purchases)
            {
                var existingPurchase = await _stockPurchaseRepository.FirstOrDefaultAsync(
                    x => x.ClientId == purchaseDto.ClientId && x.TenantId == tenantId);

                if (existingPurchase != null)
                {
                    // Update if client has newer data
                    if (purchaseDto.LastSyncedAt > existingPurchase.LastSyncedAt)
                    {
                        existingPurchase.InventoryItemId = purchaseDto.InventoryItemId;
                        existingPurchase.InventoryItemName = purchaseDto.InventoryItemName;
                        existingPurchase.Emoji = purchaseDto.Emoji;
                        existingPurchase.Quantity = purchaseDto.Quantity;
                        existingPurchase.Unit = purchaseDto.Unit;
                        existingPurchase.CostPerUnit = purchaseDto.CostPerUnit;
                        existingPurchase.TotalCost = purchaseDto.TotalCost;
                        existingPurchase.PurchaseDate = purchaseDto.PurchaseDate;
                        existingPurchase.Note = purchaseDto.Note;
                        existingPurchase.LastSyncedAt = DateTime.UtcNow;
                        await _stockPurchaseRepository.UpdateAsync(existingPurchase);
                    }

                    result.SyncedRecords.Add(new SyncedRecordInfo
                    {
                        ClientId = purchaseDto.ClientId,
                        ServerId = existingPurchase.Id,
                        SyncedAt = DateTime.UtcNow
                    });
                }
                else
                {
                    var newPurchase = new ChaiCountStockPurchase(
                        Guid.NewGuid(),
                        tenantId,
                        purchaseDto.ClientId,
                        purchaseDto.InventoryItemName,
                        purchaseDto.Quantity,
                        purchaseDto.Unit,
                        purchaseDto.CostPerUnit,
                        purchaseDto.TotalCost,
                        purchaseDto.PurchaseDate,
                        purchaseDto.InventoryItemId,
                        purchaseDto.Emoji,
                        purchaseDto.Note);

                    await _stockPurchaseRepository.InsertAsync(newPurchase);

                    result.SyncedRecords.Add(new SyncedRecordInfo
                    {
                        ClientId = purchaseDto.ClientId,
                        ServerId = newPurchase.Id,
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
            Logger.Error("Error syncing stock purchases", ex);
        }

        return result;
    }

    /// <summary>
    /// Get stock purchases (expenses)
    /// </summary>
    public async Task<List<ChaiCountStockPurchaseDto>> GetStockPurchases(DateTime? fromDate, DateTime? toDate)
    {
        var tenantId = AbpSession.GetTenantId();

        var query = _stockPurchaseRepository.GetAll()
            .Where(x => x.TenantId == tenantId && !x.IsDeleted);

        if (fromDate.HasValue)
            query = query.Where(x => x.PurchaseDate >= fromDate.Value);
        if (toDate.HasValue)
            query = query.Where(x => x.PurchaseDate <= toDate.Value);

        var purchases = await query.OrderByDescending(x => x.PurchaseDate).ToListAsync();

        return purchases.Select(p => new ChaiCountStockPurchaseDto
        {
            Id = p.Id,
            ClientId = p.ClientId,
            InventoryItemId = p.InventoryItemId,
            InventoryItemName = p.InventoryItemName,
            Emoji = p.Emoji,
            Quantity = p.Quantity,
            Unit = p.Unit,
            CostPerUnit = p.CostPerUnit,
            TotalCost = p.TotalCost,
            PurchaseDate = p.PurchaseDate,
            Note = p.Note,
            LastSyncedAt = p.LastSyncedAt
        }).ToList();
    }

    /// <summary>
    /// Get profit report (Revenue - Expenses)
    /// </summary>
    public async Task<ProfitReportDto> GetProfitReport(GetProfitReportInput input)
    {
        var tenantId = AbpSession.GetTenantId();

        // Get sales in date range
        var sales = await _saleRepository.GetAll()
            .Include(s => s.SaleItems)
            .Where(x => x.TenantId == tenantId && !x.IsDeleted)
            .Where(x => x.SaleDate.Date >= input.FromDate.Date && x.SaleDate.Date <= input.ToDate.Date)
            .ToListAsync();

        // Get stock purchases (expenses) in date range
        var purchases = await _stockPurchaseRepository.GetAll()
            .Where(x => x.TenantId == tenantId && !x.IsDeleted)
            .Where(x => x.PurchaseDate.Date >= input.FromDate.Date && x.PurchaseDate.Date <= input.ToDate.Date)
            .ToListAsync();

        // Calculate totals
        var totalRevenue = sales.Sum(s => s.TotalAmount);
        var totalExpenses = purchases.Sum(p => p.TotalCost);
        var profit = totalRevenue - totalExpenses;
        var profitMargin = totalRevenue > 0 ? (profit / totalRevenue) * 100 : 0;

        // Revenue breakdown by item
        var allSaleItems = sales.SelectMany(s => s.SaleItems ?? new List<ChaiCountSaleItem>()).ToList();
        var revenueByItem = allSaleItems
            .GroupBy(si => si.ItemName)
            .Select(g => new RevenueByItemDto
            {
                ItemName = g.Key,
                Emoji = g.FirstOrDefault()?.ItemName?.Contains("Tea") == true ? "☕" : "🍵",
                QuantitySold = g.Sum(si => si.Quantity),
                Revenue = g.Sum(si => si.TotalAmount)
            })
            .OrderByDescending(x => x.Revenue)
            .ToList();

        // Expense breakdown by inventory item
        var expenseByItem = purchases
            .GroupBy(p => p.InventoryItemName)
            .Select(g => new ExpenseByItemDto
            {
                ItemName = g.Key,
                Emoji = g.FirstOrDefault()?.Emoji ?? "📦",
                QuantityPurchased = g.Sum(p => p.Quantity),
                Unit = g.FirstOrDefault()?.Unit ?? "",
                TotalCost = g.Sum(p => p.TotalCost)
            })
            .OrderByDescending(x => x.TotalCost)
            .ToList();

        // Daily profit trend
        var dates = Enumerable.Range(0, (input.ToDate - input.FromDate).Days + 1)
            .Select(i => input.FromDate.AddDays(i).Date)
            .ToList();

        var dailyTrend = dates.Select(date => new DailyProfitDto
        {
            Date = date,
            Revenue = sales.Where(s => s.SaleDate.Date == date).Sum(s => s.TotalAmount),
            Expenses = purchases.Where(p => p.PurchaseDate.Date == date).Sum(p => p.TotalCost),
            Profit = sales.Where(s => s.SaleDate.Date == date).Sum(s => s.TotalAmount)
                   - purchases.Where(p => p.PurchaseDate.Date == date).Sum(p => p.TotalCost)
        }).ToList();

        return new ProfitReportDto
        {
            TotalRevenue = totalRevenue,
            TotalExpenses = totalExpenses,
            Profit = profit,
            ProfitMargin = profitMargin,
            RevenueByItem = revenueByItem,
            ExpenseByItem = expenseByItem,
            DailyTrend = dailyTrend
        };
    }

    // ============ Delete APIs ============

    /// <summary>
    /// Delete specific records by client IDs
    /// </summary>
    public async Task<DeleteRecordsResult> DeleteRecords(DeleteRecordsInput input)
    {
        var result = new DeleteRecordsResult
        {
            DeletedClientIds = new List<string>(),
            FailedClientIds = new List<string>()
        };

        try
        {
            var tenantId = AbpSession.GetTenantId();

            foreach (var clientId in input.ClientIds)
            {
                try
                {
                    switch (input.RecordType.ToLower())
                    {
                        case "items":
                            var item = await _itemRepository.FirstOrDefaultAsync(
                                x => x.ClientId == clientId && x.TenantId == tenantId);
                            if (item != null) await _itemRepository.DeleteAsync(item);
                            break;

                        case "sales":
                            var sale = await _saleRepository.FirstOrDefaultAsync(
                                x => x.ClientId == clientId && x.TenantId == tenantId);
                            if (sale != null) await _saleRepository.DeleteAsync(sale);
                            break;

                        case "customers":
                            var customer = await _customerRepository.FirstOrDefaultAsync(
                                x => x.ClientId == clientId && x.TenantId == tenantId);
                            if (customer != null) await _customerRepository.DeleteAsync(customer);
                            break;

                        case "inventory":
                            var inv = await _inventoryRepository.FirstOrDefaultAsync(
                                x => x.ClientId == clientId && x.TenantId == tenantId);
                            if (inv != null) await _inventoryRepository.DeleteAsync(inv);
                            break;

                        case "offers":
                            var offer = await _offerRepository.FirstOrDefaultAsync(
                                x => x.ClientId == clientId && x.TenantId == tenantId);
                            if (offer != null) await _offerRepository.DeleteAsync(offer);
                            break;

                        default:
                            result.FailedClientIds.Add(clientId);
                            continue;
                    }

                    result.DeletedClientIds.Add(clientId);
                }
                catch
                {
                    result.FailedClientIds.Add(clientId);
                }
            }

            result.Success = result.FailedClientIds.Count == 0;
            result.DeletedCount = result.DeletedClientIds.Count;
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.ErrorMessage = ex.Message;
            Logger.Error("Error deleting records", ex);
        }

        return result;
    }

    /// <summary>
    /// Delete all ChaiCount data (factory reset)
    /// </summary>
    public async Task<DeleteRecordsResult> DeleteAllData(DeleteAllDataInput input)
    {
        var result = new DeleteRecordsResult
        {
            DeletedClientIds = new List<string>(),
            FailedClientIds = new List<string>()
        };

        if (input.ConfirmationToken != "CONFIRM_DELETE_ALL")
        {
            result.Success = false;
            result.ErrorMessage = "Invalid confirmation token. Please provide 'CONFIRM_DELETE_ALL' to confirm.";
            return result;
        }

        try
        {
            var tenantId = AbpSession.GetTenantId();
            var deletedCount = 0;

            // Delete all data for this tenant
            var items = await _itemRepository.GetAll().Where(x => x.TenantId == tenantId).ToListAsync();
            foreach (var item in items) { await _itemRepository.DeleteAsync(item); deletedCount++; }

            var sales = await _saleRepository.GetAll().Where(x => x.TenantId == tenantId).ToListAsync();
            foreach (var sale in sales) { await _saleRepository.DeleteAsync(sale); deletedCount++; }

            var saleItems = await _saleItemRepository.GetAll().Where(x => x.TenantId == tenantId).ToListAsync();
            foreach (var si in saleItems) { await _saleItemRepository.DeleteAsync(si); deletedCount++; }

            var customers = await _customerRepository.GetAll().Where(x => x.TenantId == tenantId).ToListAsync();
            foreach (var c in customers) { await _customerRepository.DeleteAsync(c); deletedCount++; }

            var inventory = await _inventoryRepository.GetAll().Where(x => x.TenantId == tenantId).ToListAsync();
            foreach (var inv in inventory) { await _inventoryRepository.DeleteAsync(inv); deletedCount++; }

            var offers = await _offerRepository.GetAll().Where(x => x.TenantId == tenantId).ToListAsync();
            foreach (var offer in offers) { await _offerRepository.DeleteAsync(offer); deletedCount++; }

            var usages = await _itemUsageRepository.GetAll().Where(x => x.TenantId == tenantId).ToListAsync();
            foreach (var usage in usages) { await _itemUsageRepository.DeleteAsync(usage); deletedCount++; }

            if (!input.KeepAccount)
            {
                var settings = await _settingsRepository.GetAll().Where(x => x.TenantId == tenantId).ToListAsync();
                foreach (var s in settings) { await _settingsRepository.DeleteAsync(s); deletedCount++; }

                var loyaltyConfig = await _loyaltyConfigRepository.GetAll().Where(x => x.TenantId == tenantId).ToListAsync();
                foreach (var lc in loyaltyConfig) { await _loyaltyConfigRepository.DeleteAsync(lc); deletedCount++; }
            }

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

    // ============ Export APIs ============

    /// <summary>
    /// Export all data
    /// </summary>
    public async Task<ExportDataOutput> ExportData(ExportDataInput input)
    {
        var tenantId = AbpSession.GetTenantId();

        var settings = await _settingsRepository.FirstOrDefaultAsync(x => x.TenantId == tenantId);
        var loyaltyConfig = await _loyaltyConfigRepository.FirstOrDefaultAsync(x => x.TenantId == tenantId);
        var items = await _itemRepository.GetAll().Where(x => x.TenantId == tenantId && !x.IsDeleted).ToListAsync();
        var customers = await _customerRepository.GetAll().Where(x => x.TenantId == tenantId && !x.IsDeleted).ToListAsync();
        var inventory = await _inventoryRepository.GetAll().Where(x => x.TenantId == tenantId && !x.IsDeleted).ToListAsync();
        var offers = await _offerRepository.GetAll().Where(x => x.TenantId == tenantId && !x.IsDeleted).ToListAsync();
        var usages = await _itemUsageRepository.GetAll().Where(x => x.TenantId == tenantId && !x.IsDeleted).ToListAsync();

        var salesQuery = _saleRepository.GetAll()
            .Include(s => s.SaleItems)
            .Where(x => x.TenantId == tenantId && !x.IsDeleted);

        if (input.FromDate.HasValue)
            salesQuery = salesQuery.Where(x => x.SaleDate >= input.FromDate.Value);
        if (input.ToDate.HasValue)
            salesQuery = salesQuery.Where(x => x.SaleDate <= input.ToDate.Value);

        var sales = await salesQuery.ToListAsync();

        return new ExportDataOutput
        {
            Format = input.Format ?? "json",
            ExportedAt = DateTime.UtcNow,
            ShopName = settings?.ShopName ?? "My Tea Shop",
            Settings = settings != null ? MapToSettingsDto(settings) : null,
            LoyaltyConfig = loyaltyConfig != null ? MapToLoyaltyConfigDto(loyaltyConfig) : null,
            Items = items.Select(x => new ChaiCountItemDto
            {
                Id = x.Id,
                ClientId = x.ClientId,
                Name = x.Name,
                Emoji = x.Emoji,
                Category = x.Category,
                Price = x.Price,
                StockQuantity = x.StockQuantity,
                MinStockLevel = x.MinStockLevel,
                DisplayOrder = x.DisplayOrder,
                IsActive = x.IsActive,
                LastSyncedAt = x.LastSyncedAt
            }).ToList(),
            Customers = customers.Select(c => new ChaiCountCustomerDto
            {
                Id = c.Id,
                ClientId = c.ClientId,
                Name = c.Name,
                Phone = c.Phone,
                LoyaltyPoints = c.LoyaltyPoints,
                TotalVisits = c.TotalVisits,
                TotalSpent = c.TotalSpent,
                LastVisitDate = c.LastVisitDate
            }).ToList(),
            Inventory = inventory.Select(i => new ChaiCountInventoryDto
            {
                Id = i.Id,
                ClientId = i.ClientId,
                Name = i.Name,
                Emoji = i.Emoji,
                Quantity = i.Quantity,
                Unit = i.Unit,
                LowStockThreshold = i.LowStockThreshold,
                CostPerUnit = i.CostPerUnit,
                IsLowStock = i.IsLowStock
            }).ToList(),
            Sales = sales.Select(s => new ChaiCountSaleDto
            {
                Id = s.Id,
                ClientId = s.ClientId,
                SaleDate = s.SaleDate,
                TotalAmount = s.TotalAmount,
                TotalItems = s.TotalItems,
                Notes = s.Notes,
                SaleItems = s.SaleItems?.Select(si => new ChaiCountSaleItemDto
                {
                    Id = si.Id,
                    ClientId = si.ClientId,
                    ItemName = si.ItemName,
                    Quantity = si.Quantity,
                    UnitPrice = si.UnitPrice,
                    TotalAmount = si.TotalAmount
                }).ToList()
            }).ToList(),
            Offers = offers.Select(MapToOfferDto).ToList(),
            ItemUsageMappings = usages.Select(u => new ChaiCountItemUsageDto
            {
                Id = u.Id,
                ClientId = u.ClientId,
                MenuItemClientId = u.MenuItemClientId,
                InventoryItemClientId = u.InventoryItemClientId,
                QuantityPerSale = u.QuantityPerSale
            }).ToList(),
            Summary = new ExportSummary
            {
                TotalItems = items.Count,
                TotalCustomers = customers.Count,
                TotalSales = sales.Count,
                TotalInventoryItems = inventory.Count,
                TotalOffers = offers.Count,
                TotalRevenue = sales.Sum(s => s.TotalAmount)
            }
        };
    }

    // ============ Helper Methods ============

    private ChaiCountShopSettingsDto MapToSettingsDto(ChaiCountShopSettings settings)
    {
        return new ChaiCountShopSettingsDto
        {
            Id = settings.Id,
            ClientId = settings.ClientId,
            ShopName = settings.ShopName,
            OwnerName = settings.OwnerName,
            PhoneNumber = settings.PhoneNumber,
            Address = settings.Address,
            UpiId = settings.UpiId,
            GoogleReviewLink = settings.GoogleReviewLink,
            ReportEmail = settings.ReportEmail,
            CurrencySymbol = settings.CurrencySymbol,
            Timezone = settings.Timezone,
            LastSyncedAt = settings.LastSyncedAt
        };
    }

    private ChaiCountLoyaltyConfigDto MapToLoyaltyConfigDto(ChaiCountLoyaltyConfig config)
    {
        return new ChaiCountLoyaltyConfigDto
        {
            Id = config.Id,
            ClientId = config.ClientId,
            IsEnabled = config.IsEnabled,
            PointsPerItem = config.PointsPerItem,
            PointsPerRupee = config.PointsPerRupee,
            RewardThreshold = config.RewardThreshold,
            RewardType = config.RewardType,
            RewardValue = config.RewardValue,
            RewardDescription = config.RewardDescription,
            PointsExpiryDays = config.PointsExpiryDays,
            LastSyncedAt = config.LastSyncedAt
        };
    }

    private ChaiCountOfferDto MapToOfferDto(ChaiCountOffer offer)
    {
        var now = DateTime.UtcNow;
        var isValid = offer.IsActive &&
            (!offer.ValidFrom.HasValue || now >= offer.ValidFrom.Value) &&
            (!offer.ValidUntil.HasValue || now <= offer.ValidUntil.Value);

        return new ChaiCountOfferDto
        {
            Id = offer.Id,
            ClientId = offer.ClientId,
            Title = offer.Title,
            Description = offer.Description,
            Code = offer.Code,
            DiscountType = offer.DiscountType,
            DiscountValue = offer.DiscountValue,
            MinimumOrderAmount = offer.MinimumOrderAmount,
            ValidFrom = offer.ValidFrom,
            ValidUntil = offer.ValidUntil,
            IsActive = offer.IsActive,
            UsageCount = offer.UsageCount,
            MaxUsageLimit = offer.MaxUsageLimit,
            LastSyncedAt = offer.LastSyncedAt,
            IsValid = isValid
        };
    }
}
