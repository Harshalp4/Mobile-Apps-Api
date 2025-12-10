namespace Bit2Sky.Bit2EHR.Authorization.Delegation;

public class UserDelegationConfiguration : IUserDelegationConfiguration
{
    public bool IsEnabled { get; set; }

    public UserDelegationConfiguration()
    {
        IsEnabled = true;
    }
}

