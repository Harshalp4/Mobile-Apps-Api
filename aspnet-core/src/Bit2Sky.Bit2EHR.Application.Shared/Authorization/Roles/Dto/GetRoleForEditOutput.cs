using System.Collections.Generic;
using Bit2Sky.Bit2EHR.Authorization.Permissions.Dto;

namespace Bit2Sky.Bit2EHR.Authorization.Roles.Dto;

public class GetRoleForEditOutput
{
    public RoleEditDto Role { get; set; }

    public List<FlatPermissionDto> Permissions { get; set; }

    public List<string> GrantedPermissionNames { get; set; }
}

