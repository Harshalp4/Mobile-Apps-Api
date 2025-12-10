using System;

namespace Bit2Sky.Bit2EHR.Authorization.Users.Profile.Cache;

[Serializable]
public class SmsVerificationCodeCacheItem
{
    public const string CacheName = "AppSmsVerificationCodeCache";

    public string Code { get; set; }

    public SmsVerificationCodeCacheItem()
    {

    }

    public SmsVerificationCodeCacheItem(string code)
    {
        Code = code;
    }
}
