using Microsoft.AspNetCore.Components;

namespace Bit2Sky.Bit2EHR.Maui.Services.Navigation;

public interface INavigationService
{
    void Initialize(NavigationManager navigationManager);

    void NavigateTo(string uri, bool forceLoad = false, bool replace = false);
}