using Abp.AspNetCore.Mvc.ViewComponents;

namespace Bit2Sky.Bit2EHR.Web.Public.Views;

public abstract class Bit2EHRViewComponent : AbpViewComponent
{
    protected Bit2EHRViewComponent()
    {
        LocalizationSourceName = Bit2EHRConsts.LocalizationSourceName;
    }
}

