using Bit2Sky.Bit2EHR.ApiClient;
using Bit2Sky.Bit2EHR.Maui.Core;
using Bit2Sky.Bit2EHR.Maui.Services.Account;
using Bit2Sky.Bit2EHR.Maui.Services.Navigation;
using Bit2Sky.Bit2EHR.Maui.Services.Storage;

namespace Bit2Sky.Bit2EHR.Maui;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new MainPage();
    }

    public static async Task OnSessionTimeout()
    {
        await DependencyResolver.Resolve<IAccountService>().LogoutAsync();
        DependencyResolver.Resolve<INavigationService>().NavigateTo(NavigationUrlConsts.Login);
    }

    public static async Task OnAccessTokenRefresh(string newAccessToken, string newEncryptedAccessToken)
    {
        await DependencyResolver.Resolve<IDataStorageService>().StoreAccessTokenAsync(newAccessToken, newEncryptedAccessToken);
    }

    public static void LoadPersistedSession()
    {
        var accessTokenManager = DependencyResolver.Resolve<IAccessTokenManager>();
        var dataStorageService = DependencyResolver.Resolve<IDataStorageService>();
        var applicationContext = DependencyResolver.Resolve<IApplicationContext>();

        accessTokenManager.AuthenticateResult = dataStorageService.RetrieveAuthenticateResult();
        applicationContext.Load(dataStorageService.RetrieveTenantInfo(), dataStorageService.RetrieveLoginInfo());
    }
}