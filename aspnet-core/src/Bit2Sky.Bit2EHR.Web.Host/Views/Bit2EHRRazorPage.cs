using Abp.AspNetCore.Mvc.Views;

namespace Bit2Sky.Bit2EHR.Web.Views;

public abstract class Bit2EHRRazorPage<TModel> : AbpRazorPage<TModel>
{
    protected Bit2EHRRazorPage()
    {
        LocalizationSourceName = Bit2EHRConsts.LocalizationSourceName;
    }
}

