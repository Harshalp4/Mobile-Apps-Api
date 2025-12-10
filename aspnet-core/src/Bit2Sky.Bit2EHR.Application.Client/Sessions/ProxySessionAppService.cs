using System.Threading.Tasks;
using Bit2Sky.Bit2EHR.Sessions.Dto;

namespace Bit2Sky.Bit2EHR.Sessions;

public class ProxySessionAppService : ProxyAppServiceBase, ISessionAppService
{
    public async Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations()
    {
        return await ApiClient.GetAsync<GetCurrentLoginInformationsOutput>(GetEndpoint(nameof(GetCurrentLoginInformations)));
    }

    public async Task<UpdateUserSignInTokenOutput> UpdateUserSignInToken()
    {
        return await ApiClient.PutAsync<UpdateUserSignInTokenOutput>(GetEndpoint(nameof(UpdateUserSignInToken)));
    }
}

