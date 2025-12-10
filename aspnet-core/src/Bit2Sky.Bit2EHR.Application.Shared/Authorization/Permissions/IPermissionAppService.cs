using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Bit2Sky.Bit2EHR.Authorization.Permissions.Dto;

namespace Bit2Sky.Bit2EHR.Authorization.Permissions;

public interface IPermissionAppService : IApplicationService
{
    ListResultDto<FlatPermissionWithLevelDto> GetAllPermissions();
}

