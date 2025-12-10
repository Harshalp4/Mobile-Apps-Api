using System.Threading.Tasks;
using Bit2Sky.Bit2EHR.Security.Recaptcha;

namespace Bit2Sky.Bit2EHR.Test.Base.Web;

public class FakeRecaptchaValidator : IRecaptchaValidator
{
    public Task ValidateAsync(string captchaResponse)
    {
        return Task.CompletedTask;
    }
}
