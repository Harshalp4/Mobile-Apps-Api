using Abp.Domain.Uow;
using Abp.EntityFrameworkCore;
using Abp.OpenIddict.EntityFrameworkCore.Tokens;
using Bit2Sky.Bit2EHR.EntityFrameworkCore;

namespace Bit2Sky.Bit2EHR.OpenIddict.Tokens;

public class OpenIddictTokenRepository : EfCoreOpenIddictTokenRepository<Bit2EHRDbContext>
{
    public OpenIddictTokenRepository(
        IDbContextProvider<Bit2EHRDbContext> dbContextProvider,
        IUnitOfWorkManager unitOfWorkManager) : base(dbContextProvider, unitOfWorkManager)
    {
    }
}

