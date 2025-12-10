namespace Bit2Sky.Bit2EHR.Sessions.Dto;

public class UpdateUserSignInTokenOutput
{
    public string SignInToken { get; set; }

    public string EncodedUserId { get; set; }

    public string EncodedTenantId { get; set; }
}

