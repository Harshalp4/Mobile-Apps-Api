using System.Globalization;
using Abp.Dependency;

namespace Bit2Sky.Bit2EHR.Localization;

public class ApplicationCulturesProvider : IApplicationCulturesProvider, ITransientDependency
{
    public CultureInfo[] GetAllCultures()
    {
        return CultureInfo.GetCultures(CultureTypes.AllCultures);
    }
}

