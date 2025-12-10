using Microsoft.AspNetCore.Components;
using Bit2Sky.Bit2EHR.Maui.Core.Components;
using Bit2Sky.Bit2EHR.Maui.Core.Threading;
using Bit2Sky.Bit2EHR.Maui.Services.UI;


namespace Bit2Sky.Bit2EHR.Maui.Pages.MySettings;

public partial class ThemeSwitch : Bit2EHRComponentBase
{
    private string _selectedTheme = ThemeService.GetUserTheme();

    private string[] _themes = ThemeService.GetAllThemes();

    public string SelectedTheme
    {
        get => _selectedTheme;
        set
        {
            _selectedTheme = value;
            AsyncRunner.Run(ThemeService.SetUserTheme(JS, SelectedTheme));
        }
    }
}