using System.ComponentModel.DataAnnotations;

namespace Bit2Sky.Bit2EHR.Maui.Models.Login;

public class ForgotPasswordModel
{
    [EmailAddress]
    [Required]
    public string EmailAddress { get; set; }
}