using System.ComponentModel.DataAnnotations;

namespace Bit2Sky.Bit2EHR.Friendships.Dto;

public class CreateFriendshipRequestInput
{
    [Range(1, long.MaxValue)]
    public long UserId { get; set; }

    public int? TenantId { get; set; }
}

