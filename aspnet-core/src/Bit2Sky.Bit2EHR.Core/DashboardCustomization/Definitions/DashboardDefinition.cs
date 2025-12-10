using System.Collections.Generic;

namespace Bit2Sky.Bit2EHR.DashboardCustomization.Definitions;

public class DashboardDefinition
{
    public string Name { get; set; }

    public List<string> AvailableWidgets { get; set; }

    public DashboardDefinition(string name, List<string> availableWidgets)
    {
        Name = name;
        AvailableWidgets = availableWidgets;
    }
}

