using Bit2Sky.Bit2EHR.Maui.Models.NavigationMenu;

namespace Bit2Sky.Bit2EHR.Maui.Services.Navigation;

public interface IMenuProvider
{
    List<NavigationMenuItem> GetAuthorizedMenuItems(Dictionary<string, string> grantedPermissions);
}