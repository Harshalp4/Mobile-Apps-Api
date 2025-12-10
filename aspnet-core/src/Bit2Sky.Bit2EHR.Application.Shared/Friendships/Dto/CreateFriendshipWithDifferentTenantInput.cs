using System.ComponentModel.DataAnnotations;

namespace Bit2Sky.Bit2EHR.Friendships.Dto;

public class CreateFriendshipWithDifferentTenantInput
{
    [Required(AllowEmptyStrings = true)]
    public string TenancyName { get; set; }

    public string UserName { get; set; }
}

