using System.Collections.Generic;
using Bit2Sky.Bit2EHR.Url;

namespace Bit2Sky.Bit2EHR.Test.Base.Url;

public class FakeWebUrlService : IWebUrlService
{
    public string WebSiteRootAddressFormat { get; }

    public string ServerRootAddressFormat { get; }

    public bool SupportsTenancyNameInUrl { get; }

    public string GetSiteRootAddress(string tenancyName = null)
    {
        return "http://test.com/";
    }

    public string GetServerRootAddress(string tenancyName = null)
    {
        return "http://test.com/";
    }

    public List<string> GetRedirectAllowedExternalWebSites()
    {
        return new List<string>();
    }
}