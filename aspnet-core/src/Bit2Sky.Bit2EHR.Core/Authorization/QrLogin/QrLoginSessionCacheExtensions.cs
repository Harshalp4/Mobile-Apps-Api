using Abp.Runtime.Caching;

namespace Bit2Sky.Bit2EHR.Authorization.QrLogin;

public static class QrLoginSessionCacheExtensions
{
    public static ITypedCache<string, QrLoginSessionIdCacheItem> GetQrLoginSessionIdCacheItem(
        this ICacheManager cacheManager)
    {
        return cacheManager.GetCache<string, QrLoginSessionIdCacheItem>(QrLoginSessionIdCacheItem
            .CacheName);
    }
}