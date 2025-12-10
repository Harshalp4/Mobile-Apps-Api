using Abp.Dependency;

namespace Bit2Sky.Bit2EHR.Maui.Services.UI;

public class LanguageService : ISingletonDependency
{
    public event EventHandler OnLanguageChanged;

    public void ChangeLanguage()
    {
        OnLanguageChanged?.Invoke(this, EventArgs.Empty);
    }
}