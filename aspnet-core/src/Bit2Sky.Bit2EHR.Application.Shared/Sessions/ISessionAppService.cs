using System.Threading.Tasks;
using Abp.Application.Services;
using Bit2Sky.Bit2EHR.Sessions.Dto;

namespace Bit2Sky.Bit2EHR.Sessions;

public interface ISessionAppService : IApplicationService
{
    Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();

    Task<UpdateUserSignInTokenOutput> UpdateUserSignInToken();
}

