using System.Collections.Generic;

namespace Bit2Sky.Bit2EHR.Authorization.Users.Profile.Dto;

public class UpdateGoogleAuthenticatorKeyOutput
{
    public IEnumerable<string> RecoveryCodes { get; set; }
}

