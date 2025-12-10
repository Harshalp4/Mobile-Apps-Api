using Microsoft.Extensions.DependencyInjection;
using Bit2Sky.Bit2EHR.HealthChecks;

namespace Bit2Sky.Bit2EHR.Web.HealthCheck;

public static class AbpZeroHealthCheck
{
    public static IHealthChecksBuilder AddAbpZeroHealthCheck(this IServiceCollection services)
    {
        var builder = services.AddHealthChecks();
        builder.AddCheck<Bit2EHRDbContextHealthCheck>("Database Connection");
        builder.AddCheck<Bit2EHRDbContextUsersHealthCheck>("Database Connection with user check");
        builder.AddCheck<CacheHealthCheck>("Cache");

        // add your custom health checks here
        // builder.AddCheck<MyCustomHealthCheck>("my health check");

        return builder;
    }
}

