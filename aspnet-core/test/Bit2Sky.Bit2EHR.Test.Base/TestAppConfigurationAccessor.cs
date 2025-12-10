using Abp.Dependency;
using Abp.Reflection.Extensions;
using Microsoft.Extensions.Configuration;
using Bit2Sky.Bit2EHR.Configuration;

namespace Bit2Sky.Bit2EHR.Test.Base;

public class TestAppConfigurationAccessor : IAppConfigurationAccessor, ISingletonDependency
{
    public IConfigurationRoot Configuration { get; }

    public TestAppConfigurationAccessor()
    {
        Configuration = AppConfigurations.Get(
            typeof(Bit2EHRTestBaseModule).GetAssembly().GetDirectoryPathOrNull()
        );
    }
}
