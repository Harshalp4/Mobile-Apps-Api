using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace Bit2Sky.Bit2EHR.Web.Public.Views;

public abstract class Bit2EHRRazorPage<TModel> : AbpRazorPage<TModel>
{
    [RazorInject]
    public IAbpSession AbpSession { get; set; }

    protected Bit2EHRRazorPage()
    {
        LocalizationSourceName = Bit2EHRConsts.LocalizationSourceName;
    }
}

