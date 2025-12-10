namespace Bit2Sky.Bit2EHR.Configuration.Host.Dto;

public class CaptchaSettingsEditDto
{
    public bool UseCaptchaOnRegistration { get; set; }

    public bool UseCaptchaOnEmailActivation { get; set; }

    public bool UseCaptchaOnResetPassword { get; set; }

    public bool UseCaptchaOnLogin { get; set; }
}

