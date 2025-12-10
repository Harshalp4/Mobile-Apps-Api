using Abp;
using Bit2Sky.Bit2EHR.Maui.Core;

namespace Bit2Sky.Bit2EHR.Maui.NativeMauiComponents;

public partial class Bit2EHRNativeMauiComponentBase : ContentPage
{
    public Bit2EHRNativeMauiComponentBase()
    {
        InitializeComponent();
    }

    protected static T Resolve<T>()
    {
        return DependencyResolver.Resolve<T>();
    }

    protected string L(string text)
    {
        return Core.Localization.L.Localize(text);
    }

    protected Page GetMainPage()
    {
        var mainPage = Application.Current?.Windows[0].Page;

        if (mainPage is null)
        {
            throw new AbpException("Main page is not set yet.");
        }

        return mainPage;
    }
}