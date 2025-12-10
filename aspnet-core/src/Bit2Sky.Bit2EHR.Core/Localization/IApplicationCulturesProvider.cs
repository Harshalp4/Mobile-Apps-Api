using System.Globalization;

namespace Bit2Sky.Bit2EHR.Localization;

public interface IApplicationCulturesProvider
{
    CultureInfo[] GetAllCultures();
}

