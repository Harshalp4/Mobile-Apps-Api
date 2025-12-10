using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Bit2Sky.Bit2EHR.Caching.Dto;

namespace Bit2Sky.Bit2EHR.Caching;

public interface ICachingAppService : IApplicationService
{
    ListResultDto<CacheDto> GetAllCaches();

    Task ClearCache(EntityDto<string> input);

    Task ClearAllCaches();

    bool CanClearAllCaches();
}

