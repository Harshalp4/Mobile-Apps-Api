using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Bit2Sky.Bit2EHR.Authorization.Users.Dto;
using Bit2Sky.Bit2EHR.Dto;

namespace Bit2Sky.Bit2EHR.Authorization.Users;

public interface IUserAppService : IApplicationService
{
    Task<PagedResultDto<UserListDto>> GetUsers(GetUsersInput input);

    Task<FileDto> GetUsersToExcel(GetUsersToExcelInput input);

    Task<List<string>> GetUserExcelColumnsToExcel();

    Task<GetUserForEditOutput> GetUserForEdit(NullableIdDto<long> input);

    Task<GetUserPermissionsForEditOutput> GetUserPermissionsForEdit(EntityDto<long> input);

    Task ResetUserSpecificPermissions(EntityDto<long> input);

    Task UpdateUserPermissions(UpdateUserPermissionsInput input);

    Task CreateOrUpdateUser(CreateOrUpdateUserInput input);

    Task DeleteUser(EntityDto<long> input);

    Task UnlockUser(EntityDto<long> input);
}

