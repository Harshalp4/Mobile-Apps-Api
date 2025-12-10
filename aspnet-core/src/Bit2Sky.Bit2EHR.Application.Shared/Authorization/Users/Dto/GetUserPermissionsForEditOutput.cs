using System.Collections.Generic;
using Bit2Sky.Bit2EHR.Authorization.Permissions.Dto;

namespace Bit2Sky.Bit2EHR.Authorization.Users.Dto;

public class GetUserPermissionsForEditOutput
{
    public List<FlatPermissionDto> Permissions { get; set; }

    public List<string> GrantedPermissionNames { get; set; }
}

