using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Bit2Sky.Bit2EHR.Authorization.Users.Dto;

namespace Bit2Sky.Bit2EHR.Authorization.Users;

public interface IUserLoginAppService : IApplicationService
{
    Task<PagedResultDto<UserLoginAttemptDto>> GetUserLoginAttempts(GetLoginAttemptsInput input);
    Task<string> GetExternalLoginProviderNameByUser(long userId);
}

