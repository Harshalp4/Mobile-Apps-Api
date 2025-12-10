using System.Collections.Generic;

namespace Bit2Sky.Bit2EHR.Authorization.Users.Profile.Dto;

public class UpdateGoogleAuthenticatorKeyInput
{
    public string GoogleAuthenticatorKey { get; set; }
    public string AuthenticatorCode { get; set; }
}

