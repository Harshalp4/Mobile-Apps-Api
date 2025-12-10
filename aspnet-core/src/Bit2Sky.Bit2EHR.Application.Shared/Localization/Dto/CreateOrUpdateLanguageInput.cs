using System.ComponentModel.DataAnnotations;

namespace Bit2Sky.Bit2EHR.Localization.Dto;

public class CreateOrUpdateLanguageInput
{
    [Required]
    public ApplicationLanguageEditDto Language { get; set; }
}

