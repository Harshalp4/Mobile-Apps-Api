using System.ComponentModel.DataAnnotations;

namespace Bit2Sky.Bit2EHR.Web.Models.TokenAuth;

public class SendTwoFactorAuthCodeModel
{
    [Range(1, long.MaxValue)]
    public long UserId { get; set; }

    [Required]
    public string Provider { get; set; }
}

