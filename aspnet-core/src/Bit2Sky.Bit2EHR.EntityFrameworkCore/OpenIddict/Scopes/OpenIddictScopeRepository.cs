using Abp.Domain.Uow;
using Abp.EntityFrameworkCore;
using Abp.OpenIddict.EntityFrameworkCore.Scopes;
using Bit2Sky.Bit2EHR.EntityFrameworkCore;

namespace Bit2Sky.Bit2EHR.OpenIddict.Scopes;

public class OpenIddictScopeRepository : EfCoreOpenIddictScopeRepository<Bit2EHRDbContext>
{
    public OpenIddictScopeRepository(
        IDbContextProvider<Bit2EHRDbContext> dbContextProvider,
        IUnitOfWorkManager unitOfWorkManager) : base(dbContextProvider, unitOfWorkManager)
    {
    }
}

