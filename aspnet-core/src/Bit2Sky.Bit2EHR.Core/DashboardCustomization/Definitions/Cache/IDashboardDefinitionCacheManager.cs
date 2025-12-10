using Abp.Dependency;

namespace Bit2Sky.Bit2EHR.DashboardCustomization.Definitions.Cache;

public interface IDashboardDefinitionCacheManager : ITransientDependency
{
    DashboardDefinition Get(string name);

    void Set(DashboardDefinition definition);
}

