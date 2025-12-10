using Abp.Dependency;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Bit2Sky.Bit2EHR.Configuration;

namespace Bit2Sky.Bit2EHR.Web.Configuration;

public class AppConfigurationAccessor : IAppConfigurationAccessor, ISingletonDependency
{
    public IConfigurationRoot Configuration { get; }

    public AppConfigurationAccessor(IWebHostEnvironment env)
    {
        Configuration = env.GetAppConfiguration();
    }
}

