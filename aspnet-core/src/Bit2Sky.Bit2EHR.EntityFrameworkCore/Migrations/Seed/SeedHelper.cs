using System;
using System.Transactions;
using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.EntityFrameworkCore.Uow;
using Abp.MultiTenancy;
using Microsoft.EntityFrameworkCore;
using Bit2Sky.Bit2EHR.EntityFrameworkCore;
using Bit2Sky.Bit2EHR.Migrations.Seed.Host;
using Bit2Sky.Bit2EHR.Migrations.Seed.Tenants;

namespace Bit2Sky.Bit2EHR.Migrations.Seed;

public static class SeedHelper
{
    public static void SeedHostDb(IIocResolver iocResolver)
    {
        WithDbContext<Bit2EHRDbContext>(iocResolver, SeedHostDb);
    }

    public static void SeedHostDb(Bit2EHRDbContext context)
    {
        context.SuppressAutoSetTenantId = true;

        //Host seed
        new InitialHostDbBuilder(context).Create();

        //Default tenant seed (in host database).
        new DefaultTenantBuilder(context).Create();
        new TenantRoleAndUserBuilder(context, 1).Create();
    }

    private static void WithDbContext<TDbContext>(IIocResolver iocResolver, Action<TDbContext> contextAction)
        where TDbContext : DbContext
    {
        using (var uowManager = iocResolver.ResolveAsDisposable<IUnitOfWorkManager>())
        {
            using (var uow = uowManager.Object.Begin(TransactionScopeOption.Suppress))
            {
                var context = uowManager.Object.Current.GetDbContext<TDbContext>(MultiTenancySides.Host);

                contextAction(context);

                uow.Complete();
            }
        }
    }
}

