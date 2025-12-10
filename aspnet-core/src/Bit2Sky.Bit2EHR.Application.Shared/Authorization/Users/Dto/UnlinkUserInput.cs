using Abp;

namespace Bit2Sky.Bit2EHR.Authorization.Users.Dto;

public class UnlinkUserInput
{
    public int? TenantId { get; set; }

    public long UserId { get; set; }

    public UserIdentifier ToUserIdentifier()
    {
        return new UserIdentifier(TenantId, UserId);
    }
}

