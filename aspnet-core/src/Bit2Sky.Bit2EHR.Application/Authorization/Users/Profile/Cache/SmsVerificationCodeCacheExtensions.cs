using Abp.Runtime.Caching;

namespace Bit2Sky.Bit2EHR.Authorization.Users.Profile.Cache;

public static class SmsVerificationCodeCacheExtensions
{
    public static ITypedCache<string, SmsVerificationCodeCacheItem> GetSmsVerificationCodeCache(this ICacheManager cacheManager)
    {
        return cacheManager.GetCache<string, SmsVerificationCodeCacheItem>(SmsVerificationCodeCacheItem.CacheName);
    }
}
