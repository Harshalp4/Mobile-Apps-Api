using System.Collections.Generic;

namespace Bit2Sky.Bit2EHR.DynamicEntityProperties;

public interface IDynamicEntityPropertyDefinitionAppService
{
    List<string> GetAllAllowedInputTypeNames();

    List<string> GetAllEntities();
}

