using Abp.Configuration;

namespace Bit2Sky.Bit2EHR.Timing.Dto;

public class GetTimezoneComboboxItemsInput
{
    public SettingScopes DefaultTimezoneScope;

    public string SelectedTimezoneId { get; set; }
}

