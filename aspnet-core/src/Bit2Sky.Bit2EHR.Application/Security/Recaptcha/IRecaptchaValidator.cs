using System.Threading.Tasks;

namespace Bit2Sky.Bit2EHR.Security.Recaptcha;

public interface IRecaptchaValidator
{
    Task ValidateAsync(string captchaResponse);
}
