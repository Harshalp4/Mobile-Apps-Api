using System;
using System.Threading;
using System.Threading.Tasks;
using Abp.Domain.Uow;
using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Bit2Sky.Bit2EHR.EntityFrameworkCore;

namespace Bit2Sky.Bit2EHR.HealthChecks;

public class Bit2EHRDbContextUsersHealthCheck : IHealthCheck
{
    private readonly IDbContextProvider<Bit2EHRDbContext> _dbContextProvider;
    private readonly IUnitOfWorkManager _unitOfWorkManager;

    public Bit2EHRDbContextUsersHealthCheck(
        IDbContextProvider<Bit2EHRDbContext> dbContextProvider,
        IUnitOfWorkManager unitOfWorkManager
        )
    {
        _dbContextProvider = dbContextProvider;
        _unitOfWorkManager = unitOfWorkManager;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
    {
        try
        {
            using (var uow = _unitOfWorkManager.Begin())
            {
                // Switching to host is necessary for single tenant mode.
                using (_unitOfWorkManager.Current.SetTenantId(null))
                {
                    var dbContext = await _dbContextProvider.GetDbContextAsync();
                    if (!await dbContext.Database.CanConnectAsync(cancellationToken))
                    {
                        return HealthCheckResult.Unhealthy(
                            "Bit2EHRDbContext could not connect to database"
                        );
                    }

                    var user = await dbContext.Users.AnyAsync(cancellationToken);
                    await uow.CompleteAsync();

                    if (user)
                    {
                        return HealthCheckResult.Healthy("Bit2EHRDbContext connected to database and checked whether user added");
                    }

                    return HealthCheckResult.Unhealthy("Bit2EHRDbContext connected to database but there is no user.");

                }
            }
        }
        catch (Exception e)
        {
            return HealthCheckResult.Unhealthy("Bit2EHRDbContext could not connect to database.", e);
        }
    }
}
