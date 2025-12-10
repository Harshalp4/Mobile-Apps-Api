using Abp.Runtime.Caching;
using Bit2Sky.Bit2EHR.Authentication.TwoFactor;

namespace Bit2Sky.Bit2EHR.Web.Authentication.TwoFactor;

public static class TwoFactorCodeCacheExtensions
{
    public static ITypedCache<string, TwoFactorCodeCacheItem> GetTwoFactorCodeCache(this ICacheManager cacheManager)
    {
        return cacheManager.GetCache<string, TwoFactorCodeCacheItem>(TwoFactorCodeCacheItem.CacheName);
    }
}

