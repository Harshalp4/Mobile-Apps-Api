using System.Threading.Tasks;
using Bit2Sky.Bit2EHR.UiCustomization;

namespace Bit2Sky.Bit2EHR.Test.Base.UiCustomization;

public class NullUiThemeCustomizerFactory : IUiThemeCustomizerFactory
{
    public Task<IUiCustomizer> GetCurrentUiCustomizer()
    {
        return Task.FromResult(new NullThemeUiCustomizer() as IUiCustomizer);
    }

    public IUiCustomizer GetUiCustomizer(string theme)
    {
        return new NullThemeUiCustomizer();
    }
}
