using Abp.Domain.Services;

namespace Bit2Sky.Bit2EHR;

public abstract class Bit2EHRDomainServiceBase : DomainService
{
    /* Add your common members for all your domain services. */

    protected Bit2EHRDomainServiceBase()
    {
        LocalizationSourceName = Bit2EHRConsts.LocalizationSourceName;
    }
}

