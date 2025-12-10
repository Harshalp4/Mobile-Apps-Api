using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Bit2Sky.Bit2EHR.EntityFrameworkCore;

namespace Bit2Sky.Bit2EHR.HealthChecks;

public class Bit2EHRDbContextHealthCheck : IHealthCheck
{
    private readonly DatabaseCheckHelper _checkHelper;

    public Bit2EHRDbContextHealthCheck(DatabaseCheckHelper checkHelper)
    {
        _checkHelper = checkHelper;
    }

    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
    {
        if (_checkHelper.Exist("db"))
        {
            return Task.FromResult(HealthCheckResult.Healthy("Bit2EHRDbContext connected to database."));
        }

        return Task.FromResult(HealthCheckResult.Unhealthy("Bit2EHRDbContext could not connect to database"));
    }
}
