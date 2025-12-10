using System.Collections.Generic;
using Abp.Dependency;

namespace Bit2Sky.Bit2EHR.DashboardCustomization.Definitions.Cache;

public interface IWidgetFilterDefinitionCacheManager : ITransientDependency
{
    List<WidgetFilterDefinition> GetAll();

    void Set(List<WidgetFilterDefinition> definition);
}

