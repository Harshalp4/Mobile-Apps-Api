using System.Collections.Generic;

namespace Bit2Sky.Bit2EHR.EntityHistory;

public class EntityHistoryUiSetting
{
    public bool IsEnabled { get; set; }

    public List<string> EnabledEntities { get; set; }
}

