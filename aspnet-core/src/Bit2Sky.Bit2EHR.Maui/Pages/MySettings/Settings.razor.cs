using Bit2Sky.Bit2EHR.Maui.Core.Components;
using Bit2Sky.Bit2EHR.Maui.Pages.Layout;
using Bit2Sky.Bit2EHR.Maui.Services.Account;
using Bit2Sky.Bit2EHR.Maui.Services.Navigation;

namespace Bit2Sky.Bit2EHR.Maui.Pages.MySettings;

public partial class Settings : Bit2EHRMainLayoutPageComponentBase
{
    protected IAccountService AccountService { get; set; }
    protected NavMenu NavMenu { get; set; }

    protected INavigationService navigationService { get; set; }
    ChangePasswordModal changePasswordModal;

    public Settings()
    {
        AccountService = Resolve<IAccountService>();
        navigationService = Resolve<INavigationService>();
    }

    protected override async Task OnInitializedAsync()
    {
        await SetPageHeader(L("MySettings"));
    }

    private async Task LogOut()
    {
        await AccountService.LogoutAsync();
        navigationService.NavigateTo(NavigationUrlConsts.Login);
    }

    private async Task OnChangePasswordAsync()
    {
        await changePasswordModal.Hide();
        await Task.Delay(300);
        await LogOut();
    }

    private async Task OnLanguageSwitchAsync()
    {
        await SetPageHeader(L("MySettings"));
        StateHasChanged();
    }

    private async Task ChangePassword()
    {
        await changePasswordModal.Show();
    }

}