using System.Collections.ObjectModel;

namespace Bit2Sky.Bit2EHR.Editions.Dto;

//Mapped in CustomDtoMapper
public class LocalizableComboboxItemSourceDto
{
    public Collection<LocalizableComboboxItemDto> Items { get; set; }
}

