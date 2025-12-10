using System.Collections.Generic;

namespace Bit2Sky.Bit2EHR.Authorization.Users.Profile.Dto;

public class GenerateGoogleAuthenticatorKeyOutput
{
    public string QrCodeSetupImageUrl { get; set; }
    public string GoogleAuthenticatorKey { get; set; }
}

