using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;

namespace Bit2Sky.Bit2EHR.Web.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseBit2EHRForwardedHeaders(this IApplicationBuilder builder)
    {
        var options = new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
        };

        options.KnownNetworks.Clear();
        options.KnownProxies.Clear();

        return builder.UseForwardedHeaders(options);
    }
}

