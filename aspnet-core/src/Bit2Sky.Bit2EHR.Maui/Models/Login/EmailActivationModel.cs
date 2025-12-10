using System.ComponentModel.DataAnnotations;

namespace Bit2Sky.Bit2EHR.Maui.Models.Login;

public class EmailActivationModel
{
    [Required]
    [EmailAddress]
    public string EmailAddress { get; set; }
}