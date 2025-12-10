using Abp.Dependency;
using Bit2Sky.Bit2EHR.Configuration;
using Bit2Sky.Bit2EHR.Url;
using Bit2Sky.Bit2EHR.Web.Url;

namespace Bit2Sky.Bit2EHR.Web.Public.Url;

public class WebUrlService : WebUrlServiceBase, IWebUrlService, ITransientDependency
{
    public WebUrlService(
        IAppConfigurationAccessor appConfigurationAccessor) :
        base(appConfigurationAccessor)
    {
    }

    public override string WebSiteRootAddressFormatKey => "App:WebSiteRootAddress";

    public override string ServerRootAddressFormatKey => "App:AdminWebSiteRootAddress";
}

