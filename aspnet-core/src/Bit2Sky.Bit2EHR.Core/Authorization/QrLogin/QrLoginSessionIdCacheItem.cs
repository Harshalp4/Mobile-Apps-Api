using System;

namespace Bit2Sky.Bit2EHR.Authorization.QrLogin;

public class QrLoginSessionIdCacheItem
{
    public const string CacheName = "AppQrLoginSessionIdCache";

    public string Code { get; set; }

    public static readonly TimeSpan DefaultSlidingExpireTime = TimeSpan.FromMinutes(1);

    public QrLoginSessionIdCacheItem(string code)
    {
        Code = code;
    }
}

