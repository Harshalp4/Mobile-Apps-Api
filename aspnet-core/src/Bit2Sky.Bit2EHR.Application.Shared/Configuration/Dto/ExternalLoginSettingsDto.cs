using System.Collections.Generic;

namespace Bit2Sky.Bit2EHR.Configuration.Dto;

public class ExternalLoginSettingsDto
{
    public List<string> EnabledSocialLoginSettings { get; set; } = new List<string>();
}

