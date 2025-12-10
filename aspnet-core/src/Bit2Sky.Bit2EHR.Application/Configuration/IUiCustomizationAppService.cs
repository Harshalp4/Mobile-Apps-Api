using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Bit2Sky.Bit2EHR.Configuration.Dto;

namespace Bit2Sky.Bit2EHR.Configuration;

public interface IUiCustomizationSettingsAppService : IApplicationService
{
    Task<List<ThemeSettingsDto>> GetUiManagementSettings();

    Task UpdateUiManagementSettings(ThemeSettingsDto settings);

    Task UpdateDefaultUiManagementSettings(ThemeSettingsDto settings);

    Task UseSystemDefaultSettings();

    Task ChangeDarkModeOfCurrentTheme(bool isDarkModeActive);
}
