using Bit2Sky.Bit2EHR.Maui.Services.Navigation;
using Bit2Sky.Bit2EHR.Maui.Services.Permission;
using Bit2Sky.Bit2EHR.Maui.Services.UI;

namespace Bit2Sky.Bit2EHR.Maui.Core.Components;

public class Bit2EHRMainLayoutPageComponentBase : Bit2EHRComponentBase
{
    protected PageHeaderService PageHeaderService { get; set; }

    protected DomManipulatorService DomManipulatorService { get; set; }

    protected INavigationService NavigationService { get; set; }

    protected IPermissionService PermissionService { get; set; }

    public Bit2EHRMainLayoutPageComponentBase()
    {
        PageHeaderService = Resolve<PageHeaderService>();
        DomManipulatorService = Resolve<DomManipulatorService>();
        NavigationService = Resolve<INavigationService>();
        PermissionService = Resolve<IPermissionService>();
    }

    protected async Task SetPageHeader(string title)
    {
        PageHeaderService.Title = title;
        PageHeaderService.SubTitle = string.Empty;
        PageHeaderService.ClearButton();
        await DomManipulatorService.ClearModalBackdrop(JS);
    }

    protected async Task SetPageHeader(string title, string subTitle)
    {
        PageHeaderService.Title = title;
        PageHeaderService.SubTitle = subTitle;
        PageHeaderService.ClearButton();
        await DomManipulatorService.ClearModalBackdrop(JS);
    }

    protected async Task SetPageHeader(string title, string subTitle, List<PageHeaderButton> buttons)
    {
        PageHeaderService.Title = title;
        PageHeaderService.SubTitle = subTitle;
        PageHeaderService.SetButtons(buttons);
        await DomManipulatorService.ClearModalBackdrop(JS);
    }
}