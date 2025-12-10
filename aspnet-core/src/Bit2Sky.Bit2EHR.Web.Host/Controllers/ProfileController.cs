using Abp.AspNetCore.Mvc.Authorization;
using Bit2Sky.Bit2EHR.Authorization.Users.Profile;
using Bit2Sky.Bit2EHR.Storage;

namespace Bit2Sky.Bit2EHR.Web.Controllers;

[AbpMvcAuthorize]
public class ProfileController : ProfileControllerBase
{
    public ProfileController(
        ITempFileCacheManager tempFileCacheManager,
        IProfileAppService profileAppService) :
        base(tempFileCacheManager, profileAppService)
    {
    }
}

