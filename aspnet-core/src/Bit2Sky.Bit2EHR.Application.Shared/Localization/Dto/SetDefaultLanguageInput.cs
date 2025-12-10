using System.ComponentModel.DataAnnotations;
using Abp.Localization;

namespace Bit2Sky.Bit2EHR.Localization.Dto;

public class SetDefaultLanguageInput
{
    [Required]
    [StringLength(ApplicationLanguage.MaxNameLength)]
    public virtual string Name { get; set; }
}

