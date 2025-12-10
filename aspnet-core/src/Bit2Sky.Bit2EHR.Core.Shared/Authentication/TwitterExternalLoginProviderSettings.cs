using Abp.Extensions;

namespace Bit2Sky.Bit2EHR.Authentication;

public class TwitterExternalLoginProviderSettings
{
    public string ConsumerKey { get; set; }
    public string ConsumerSecret { get; set; }

    public bool IsValid()
    {
        return !ConsumerKey.IsNullOrWhiteSpace() && !ConsumerSecret.IsNullOrWhiteSpace();
    }
}


