using Abp.Domain.Uow;
using Abp.EntityFrameworkCore;
using Abp.OpenIddict.EntityFrameworkCore.Applications;
using Bit2Sky.Bit2EHR.EntityFrameworkCore;

namespace Bit2Sky.Bit2EHR.OpenIddict.Applications;

public class OpenIddictApplicationRepository : EfCoreOpenIddictApplicationRepository<Bit2EHRDbContext>
{
    public OpenIddictApplicationRepository(
        IDbContextProvider<Bit2EHRDbContext> dbContextProvider,
        IUnitOfWorkManager unitOfWorkManager) : base(dbContextProvider, unitOfWorkManager)
    {
    }
}

