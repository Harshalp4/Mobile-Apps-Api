using Abp.Application.Services.Dto;

namespace Bit2Sky.Bit2EHR.Editions.Dto;

public class SubscribableEditionComboboxItemDto : ComboboxItemDto
{
    public bool? IsFree { get; set; }

    public SubscribableEditionComboboxItemDto(string value, string displayText, bool? isFree) : base(value, displayText)
    {
        IsFree = isFree;
    }
}

