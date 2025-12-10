using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bit2Sky.Bit2EHR.Authorization.Users.Dto;

public class UpdateUserPermissionsInput
{
    [Range(1, int.MaxValue)]
    public long Id { get; set; }

    [Required]
    public List<string> GrantedPermissionNames { get; set; }
}

