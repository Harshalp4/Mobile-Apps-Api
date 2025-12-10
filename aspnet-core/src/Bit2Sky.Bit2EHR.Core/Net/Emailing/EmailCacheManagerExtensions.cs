using Abp.Runtime.Caching;

namespace Bit2Sky.Bit2EHR.Net.Emailing;

public static class EmailCacheManagerExtensions
{
    public static ITypedCache<string, EmailTemplateCacheItem> GetEmailTemplateCache(this ICacheManager cacheManager)
    {
        return cacheManager.GetCache<string, EmailTemplateCacheItem>(EmailTemplateCacheItem.CacheName);
    }
}