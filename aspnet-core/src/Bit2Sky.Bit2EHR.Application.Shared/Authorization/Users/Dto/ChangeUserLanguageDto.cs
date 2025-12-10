using System.ComponentModel.DataAnnotations;

namespace Bit2Sky.Bit2EHR.Authorization.Users.Dto;

public class ChangeUserLanguageDto
{
    [Required]
    public string LanguageName { get; set; }
}

