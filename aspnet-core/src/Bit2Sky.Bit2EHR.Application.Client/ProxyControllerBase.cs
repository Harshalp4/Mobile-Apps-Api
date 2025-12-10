using Abp.Dependency;
using Abp.Extensions;
using Bit2Sky.Bit2EHR.ApiClient;

namespace Bit2Sky.Bit2EHR;

public abstract class ProxyControllerBase : ITransientDependency
{
    public AbpApiClient ApiClient { get; set; }

    private readonly string _serviceUrlSegment;

    protected ProxyControllerBase()
    {
        _serviceUrlSegment = GetServiceUrlSegmentByConvention();
    }

    protected string GetEndpoint(string controllerName)
    {
        return _serviceUrlSegment + "/" + controllerName;
    }

    private string GetServiceUrlSegmentByConvention()
    {
        return GetType()
            .Name
            .RemovePreFix("Proxy")
            .RemovePostFix("ControllerService");
    }
}

