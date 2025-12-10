using Abp.Domain.Uow;
using Abp.EntityFrameworkCore;
using Abp.OpenIddict.EntityFrameworkCore.Authorizations;
using Bit2Sky.Bit2EHR.EntityFrameworkCore;

namespace Bit2Sky.Bit2EHR.OpenIddict.Authorizations;

public class OpenIddictAuthorizationRepository : EfCoreOpenIddictAuthorizationRepository<Bit2EHRDbContext>
{
    public OpenIddictAuthorizationRepository(
        IDbContextProvider<Bit2EHRDbContext> dbContextProvider,
        IUnitOfWorkManager unitOfWorkManager) : base(dbContextProvider, unitOfWorkManager)
    {
    }
}

