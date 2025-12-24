using Abp.Json;
using Abp.OpenIddict.Applications;
using Abp.OpenIddict.Authorizations;
using Abp.OpenIddict.EntityFrameworkCore;
using Abp.OpenIddict.Scopes;
using Abp.OpenIddict.Tokens;
using Abp.Zero.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Bit2Sky.Bit2EHR.Authorization.Delegation;
using Bit2Sky.Bit2EHR.Authorization.Roles;
using Bit2Sky.Bit2EHR.Authorization.Users;
using Bit2Sky.Bit2EHR.Chat;
using Bit2Sky.Bit2EHR.Editions;
using Bit2Sky.Bit2EHR.ExtraProperties;
using Bit2Sky.Bit2EHR.Friendships;
using Bit2Sky.Bit2EHR.MultiTenancy;
using Bit2Sky.Bit2EHR.MultiTenancy.Accounting;
using Bit2Sky.Bit2EHR.MultiTenancy.Payments;
using Bit2Sky.Bit2EHR.Storage;
using Bit2Sky.Bit2EHR.Authorization.Users;
using Bit2Sky.Bit2EHR.ChaiCount;
using Bit2Sky.Bit2EHR.FinTrack;

namespace Bit2Sky.Bit2EHR.EntityFrameworkCore;

public class Bit2EHRDbContext : AbpZeroDbContext<Tenant, Role, User, Bit2EHRDbContext>, IOpenIddictDbContext
{
    /* Define an IDbSet for each entity of the application */

    public virtual DbSet<OpenIddictApplication> Applications { get; }

    public virtual DbSet<OpenIddictAuthorization> Authorizations { get; }

    public virtual DbSet<OpenIddictScope> Scopes { get; }

    public virtual DbSet<OpenIddictToken> Tokens { get; }

    public virtual DbSet<BinaryObject> BinaryObjects { get; set; }

    public virtual DbSet<Friendship> Friendships { get; set; }

    public virtual DbSet<ChatMessage> ChatMessages { get; set; }

    public virtual DbSet<SubscribableEdition> SubscribableEditions { get; set; }

    public virtual DbSet<SubscriptionPayment> SubscriptionPayments { get; set; }

    public virtual DbSet<SubscriptionPaymentProduct> SubscriptionPaymentProducts { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<UserDelegation> UserDelegations { get; set; }

    public virtual DbSet<RecentPassword> RecentPasswords { get; set; }

	public virtual DbSet<UserAccountLink> UserAccountLinks { get; set; }

    // ChaiCount entities
    public virtual DbSet<ChaiCountItem> ChaiCountItems { get; set; }
    public virtual DbSet<ChaiCountSale> ChaiCountSales { get; set; }
    public virtual DbSet<ChaiCountSaleItem> ChaiCountSaleItems { get; set; }
    public virtual DbSet<ChaiCountCustomer> ChaiCountCustomers { get; set; }
    public virtual DbSet<ChaiCountInventory> ChaiCountInventory { get; set; }
    public virtual DbSet<ChaiCountShopSettings> ChaiCountShopSettings { get; set; }
    public virtual DbSet<ChaiCountOffer> ChaiCountOffers { get; set; }
    public virtual DbSet<ChaiCountLoyaltyConfig> ChaiCountLoyaltyConfig { get; set; }
    public virtual DbSet<ChaiCountItemUsage> ChaiCountItemUsage { get; set; }
    public virtual DbSet<ChaiCountStockPurchase> ChaiCountStockPurchases { get; set; }

    // FinTrack entities
    public virtual DbSet<FtBankAccount> FtBankAccounts { get; set; }
    public virtual DbSet<FtCategory> FtCategories { get; set; }
    public virtual DbSet<FtTransaction> FtTransactions { get; set; }
    public virtual DbSet<FtAsset> FtAssets { get; set; }
    public virtual DbSet<FtLiability> FtLiabilities { get; set; }
    public virtual DbSet<FtParty> FtParties { get; set; }
    public virtual DbSet<FtExpectedIncome> FtExpectedIncomes { get; set; }
    public virtual DbSet<FtFixedExpense> FtFixedExpenses { get; set; }
    public virtual DbSet<FtBudgetCategory> FtBudgetCategories { get; set; }

    public Bit2EHRDbContext(DbContextOptions<Bit2EHRDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<BinaryObject>(b => { b.HasIndex(e => new { e.TenantId }); });

        modelBuilder.Entity<SubscribableEdition>(b =>
        {
            b.Property(e => e.MonthlyPrice).HasPrecision(18, 2);
            b.Property(e => e.AnnualPrice).HasPrecision(18, 2);
        });

        modelBuilder.Entity<SubscriptionPayment>(x =>
        {
            x.Property(u => u.ExtraProperties)
                .HasConversion(
                    d => d.ToJsonString(false, false),
                    s => s.FromJsonString<ExtraPropertyDictionary>()
                )
                .Metadata.SetValueComparer(new ValueComparer<ExtraPropertyDictionary>(
                    (c1, c2) => c1.ToJsonString(false, false) == c2.ToJsonString(false, false),
                    c => c.ToJsonString(false, false).GetHashCode(),
                    c => c.ToJsonString(false, false).FromJsonString<ExtraPropertyDictionary>()
                ));
        });

        modelBuilder.Entity<SubscriptionPaymentProduct>(x =>
        {
            x.Property(u => u.ExtraProperties)
                .HasConversion(
                    d => d.ToJsonString(false, false),
                    s => s.FromJsonString<ExtraPropertyDictionary>()
                )
                .Metadata.SetValueComparer(new ValueComparer<ExtraPropertyDictionary>(
                    (c1, c2) => c1.ToJsonString(false, false) == c2.ToJsonString(false, false),
                    c => c.ToJsonString(false, false).GetHashCode(),
                    c => c.ToJsonString(false, false).FromJsonString<ExtraPropertyDictionary>()
                ));

            x.Property(e => e.Amount).HasPrecision(18, 2);
            x.Property(e => e.TotalAmount).HasPrecision(18, 2);
        });

        modelBuilder.Entity<ChatMessage>(b =>
        {
            b.HasIndex(e => new { e.TenantId, e.UserId, e.ReadState });
            b.HasIndex(e => new { e.TenantId, e.TargetUserId, e.ReadState });
            b.HasIndex(e => new { e.TargetTenantId, e.TargetUserId, e.ReadState });
            b.HasIndex(e => new { e.TargetTenantId, e.UserId, e.ReadState });
        });

        modelBuilder.Entity<Friendship>(b =>
        {
            b.HasIndex(e => new { e.TenantId, e.UserId });
            b.HasIndex(e => new { e.TenantId, e.FriendUserId });
            b.HasIndex(e => new { e.FriendTenantId, e.UserId });
            b.HasIndex(e => new { e.FriendTenantId, e.FriendUserId });
        });

        modelBuilder.Entity<Tenant>(b =>
        {
            b.HasIndex(e => new { e.SubscriptionEndDateUtc });
            b.HasIndex(e => new { e.CreationTime });
        });

        modelBuilder.Entity<SubscriptionPayment>(b =>
        {
            b.HasIndex(e => new { e.Status, e.CreationTime });
            b.HasIndex(e => new { PaymentId = e.ExternalPaymentId, e.Gateway });
        });

        modelBuilder.Entity<UserDelegation>(b =>
        {
            b.HasIndex(e => new { e.TenantId, e.SourceUserId });
            b.HasIndex(e => new { e.TenantId, e.TargetUserId });
        });

		modelBuilder.Entity<UserAccountLink>(b =>
		{
			b.HasIndex(e => new { e.UserAccountId, e.LinkedUserAccountId }).IsUnique();
		});

        modelBuilder.ConfigureOpenIddict();

        // ChaiCount entity configurations
        modelBuilder.Entity<ChaiCountItem>(b =>
        {
            b.HasIndex(e => new { e.TenantId, e.ClientId }).IsUnique();
            b.HasIndex(e => new { e.TenantId, e.IsActive });
            b.Property(e => e.Price).HasPrecision(18, 2);
        });

        modelBuilder.Entity<ChaiCountSale>(b =>
        {
            b.HasIndex(e => new { e.TenantId, e.ClientId }).IsUnique();
            b.HasIndex(e => new { e.TenantId, e.SaleDate });
            b.Property(e => e.TotalAmount).HasPrecision(18, 2);
        });

        modelBuilder.Entity<ChaiCountSaleItem>(b =>
        {
            b.HasIndex(e => new { e.TenantId, e.ClientId }).IsUnique();
            b.HasIndex(e => new { e.SaleId });
            b.Property(e => e.UnitPrice).HasPrecision(18, 2);
            b.Property(e => e.TotalAmount).HasPrecision(18, 2);
        });

        modelBuilder.Entity<ChaiCountCustomer>(b =>
        {
            b.HasIndex(e => new { e.TenantId, e.ClientId }).IsUnique();
            b.HasIndex(e => new { e.TenantId, e.Phone });
            b.Property(e => e.TotalSpent).HasPrecision(18, 2);
        });

        modelBuilder.Entity<ChaiCountInventory>(b =>
        {
            b.HasIndex(e => new { e.TenantId, e.ClientId }).IsUnique();
            b.Property(e => e.Quantity).HasPrecision(18, 2);
            b.Property(e => e.LowStockThreshold).HasPrecision(18, 2);
            b.Property(e => e.CostPerUnit).HasPrecision(18, 2);
        });

        modelBuilder.Entity<ChaiCountShopSettings>(b =>
        {
            b.HasIndex(e => new { e.TenantId }).IsUnique();
        });

        modelBuilder.Entity<ChaiCountOffer>(b =>
        {
            b.HasIndex(e => new { e.TenantId, e.ClientId }).IsUnique();
            b.HasIndex(e => new { e.TenantId, e.IsActive });
            b.Property(e => e.DiscountValue).HasPrecision(18, 2);
            b.Property(e => e.MinimumOrderAmount).HasPrecision(18, 2);
        });

        modelBuilder.Entity<ChaiCountLoyaltyConfig>(b =>
        {
            b.HasIndex(e => new { e.TenantId }).IsUnique();
            b.Property(e => e.PointsPerRupee).HasPrecision(18, 2);
            b.Property(e => e.RewardValue).HasPrecision(18, 2);
        });

        modelBuilder.Entity<ChaiCountItemUsage>(b =>
        {
            b.HasIndex(e => new { e.TenantId, e.ClientId }).IsUnique();
            b.HasIndex(e => new { e.TenantId, e.MenuItemClientId });
            b.Property(e => e.QuantityPerSale).HasPrecision(18, 4);
        });

        modelBuilder.Entity<ChaiCountStockPurchase>(b =>
        {
            b.HasIndex(e => new { e.TenantId, e.ClientId }).IsUnique();
            b.HasIndex(e => new { e.TenantId, e.PurchaseDate });
            b.HasIndex(e => new { e.TenantId, e.InventoryItemId });
            b.Property(e => e.Quantity).HasPrecision(18, 2);
            b.Property(e => e.CostPerUnit).HasPrecision(18, 2);
            b.Property(e => e.TotalCost).HasPrecision(18, 2);
        });

        // FinTrack entity configurations
        modelBuilder.Entity<FtBankAccount>(b =>
        {
            b.HasIndex(e => new { e.TenantId, e.ClientId }).IsUnique();
            b.HasIndex(e => new { e.TenantId, e.IsDefault });
            b.Property(e => e.Balance).HasPrecision(18, 2);
            b.Property(e => e.OpeningBalance).HasPrecision(18, 2);
        });

        modelBuilder.Entity<FtCategory>(b =>
        {
            b.HasIndex(e => new { e.TenantId, e.ClientId }).IsUnique();
            b.HasIndex(e => new { e.TenantId, e.CategoryType });
        });

        modelBuilder.Entity<FtTransaction>(b =>
        {
            b.HasIndex(e => new { e.TenantId, e.ClientId }).IsUnique();
            b.HasIndex(e => new { e.TenantId, e.TransactionDate });
            b.HasIndex(e => new { e.TenantId, e.AccountId });
            b.HasIndex(e => new { e.TenantId, e.CategoryId });
            b.HasIndex(e => new { e.TenantId, e.PartyId });
            b.Property(e => e.Amount).HasPrecision(18, 2);
        });

        modelBuilder.Entity<FtAsset>(b =>
        {
            b.HasIndex(e => new { e.TenantId, e.ClientId }).IsUnique();
            b.Property(e => e.TotalValue).HasPrecision(18, 2);
            b.Property(e => e.PaidAmount).HasPrecision(18, 2);
            b.Property(e => e.PendingAmount).HasPrecision(18, 2);
        });

        modelBuilder.Entity<FtLiability>(b =>
        {
            b.HasIndex(e => new { e.TenantId, e.ClientId }).IsUnique();
            b.Property(e => e.Principal).HasPrecision(18, 2);
            b.Property(e => e.PaidAmount).HasPrecision(18, 2);
            b.Property(e => e.PendingAmount).HasPrecision(18, 2);
            b.Property(e => e.InterestRate).HasPrecision(5, 2);
            b.Property(e => e.Emi).HasPrecision(18, 2);
        });

        modelBuilder.Entity<FtParty>(b =>
        {
            b.HasIndex(e => new { e.TenantId, e.ClientId }).IsUnique();
            b.HasIndex(e => new { e.TenantId, e.Name });
            b.Property(e => e.OwesYou).HasPrecision(18, 2);
            b.Property(e => e.YouOwe).HasPrecision(18, 2);
            b.Property(e => e.NetBalance).HasPrecision(18, 2);
        });

        modelBuilder.Entity<FtExpectedIncome>(b =>
        {
            b.HasIndex(e => new { e.TenantId, e.ClientId }).IsUnique();
            b.HasIndex(e => new { e.TenantId, e.NextDate });
            b.Property(e => e.Amount).HasPrecision(18, 2);
        });

        modelBuilder.Entity<FtFixedExpense>(b =>
        {
            b.HasIndex(e => new { e.TenantId, e.ClientId }).IsUnique();
            b.HasIndex(e => new { e.TenantId, e.Month, e.Year });
            b.Property(e => e.Amount).HasPrecision(18, 2);
        });

        modelBuilder.Entity<FtBudgetCategory>(b =>
        {
            b.HasIndex(e => new { e.TenantId, e.ClientId }).IsUnique();
            b.HasIndex(e => new { e.TenantId, e.Month, e.Year });
            b.Property(e => e.AllocatedAmount).HasPrecision(18, 2);
            b.Property(e => e.SpentAmount).HasPrecision(18, 2);
        });
    }
}