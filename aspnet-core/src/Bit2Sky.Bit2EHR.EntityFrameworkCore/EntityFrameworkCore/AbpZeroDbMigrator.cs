using Abp.Domain.Uow;
using Abp.EntityFrameworkCore;
using Abp.MultiTenancy;
using Abp.Zero.EntityFrameworkCore;

namespace Bit2Sky.Bit2EHR.EntityFrameworkCore;

public class AbpZeroDbMigrator : AbpZeroDbMigrator<Bit2EHRDbContext>
{
    public AbpZeroDbMigrator(
        IUnitOfWorkManager unitOfWorkManager,
        IDbPerTenantConnectionStringResolver connectionStringResolver,
        IDbContextResolver dbContextResolver) :
        base(
            unitOfWorkManager,
            connectionStringResolver,
            dbContextResolver)
    {

    }
}

