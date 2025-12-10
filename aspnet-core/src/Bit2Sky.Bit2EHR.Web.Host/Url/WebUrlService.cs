using Abp.Dependency;
using Bit2Sky.Bit2EHR.Configuration;
using Bit2Sky.Bit2EHR.Url;

namespace Bit2Sky.Bit2EHR.Web.Url;

public class WebUrlService : WebUrlServiceBase, IWebUrlService, ITransientDependency
{
    public WebUrlService(
        IAppConfigurationAccessor configurationAccessor) :
        base(configurationAccessor)
    {
    }

    public override string WebSiteRootAddressFormatKey => "App:ClientRootAddress";

    public override string ServerRootAddressFormatKey => "App:ServerRootAddress";
}

