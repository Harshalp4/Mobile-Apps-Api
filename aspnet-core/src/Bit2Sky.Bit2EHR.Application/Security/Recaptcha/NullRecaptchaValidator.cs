using System.Threading.Tasks;

namespace Bit2Sky.Bit2EHR.Security.Recaptcha;

public class NullRecaptchaValidator : IRecaptchaValidator
{
    public static NullRecaptchaValidator Instance { get; } = new NullRecaptchaValidator();

    public Task ValidateAsync(string captchaResponse)
    {
        return Task.CompletedTask;
    }
}
