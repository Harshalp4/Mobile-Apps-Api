using System.Threading.Tasks;
using Abp.Dependency;

namespace Bit2Sky.Bit2EHR.UiCustomization;

public interface IUiThemeCustomizerFactory : ISingletonDependency
{
    Task<IUiCustomizer> GetCurrentUiCustomizer();

    IUiCustomizer GetUiCustomizer(string theme);
}
