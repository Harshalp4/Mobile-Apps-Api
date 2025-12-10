using Abp;

namespace Bit2Sky.Bit2EHR;

/// <summary>
/// This class can be used as a base class for services in this application.
/// It has some useful objects property-injected and has some basic methods most of services may need to.
/// It's suitable for non domain nor application service classes.
/// For domain services inherit <see cref="Bit2EHRDomainServiceBase"/>.
/// For application services inherit Bit2EHRAppServiceBase.
/// </summary>
public abstract class Bit2EHRServiceBase : AbpServiceBase
{
    protected Bit2EHRServiceBase()
    {
        LocalizationSourceName = Bit2EHRConsts.LocalizationSourceName;
    }
}

