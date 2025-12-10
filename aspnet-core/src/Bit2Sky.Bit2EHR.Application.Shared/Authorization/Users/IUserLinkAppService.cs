using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Bit2Sky.Bit2EHR.Authorization.Users.Dto;

namespace Bit2Sky.Bit2EHR.Authorization.Users;

public interface IUserLinkAppService : IApplicationService
{
    Task LinkToUser(LinkToUserInput linkToUserInput);

    Task<PagedResultDto<LinkedUserDto>> GetLinkedUsers(GetLinkedUsersInput input);

    Task<ListResultDto<LinkedUserDto>> GetRecentlyUsedLinkedUsers();

    Task UnlinkUser(UnlinkUserInput input);
}

