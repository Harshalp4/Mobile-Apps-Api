using System.Threading.Tasks;
using Abp;
using Bit2Sky.Bit2EHR.Configuration.Dto;
using Bit2Sky.Bit2EHR.UiCustomization;
using Bit2Sky.Bit2EHR.UiCustomization.Dto;

namespace Bit2Sky.Bit2EHR.Test.Base.UiCustomization;

public class NullThemeUiCustomizer : IUiCustomizer
{
    public Task<UiCustomizationSettingsDto> GetUiSettings()
    {
        return Task.FromResult(new UiCustomizationSettingsDto());
    }

    public Task UpdateUserUiManagementSettingsAsync(UserIdentifier user, ThemeSettingsDto settings)
    {
        throw new System.NotImplementedException();
    }

    public Task UpdateTenantUiManagementSettingsAsync(int tenantId, ThemeSettingsDto settings, UserIdentifier changerUser)
    {
        throw new System.NotImplementedException();
    }

    public Task UpdateApplicationUiManagementSettingsAsync(ThemeSettingsDto settings, UserIdentifier changerUser)
    {
        throw new System.NotImplementedException();
    }

    public Task<ThemeSettingsDto> GetHostUiManagementSettings()
    {
        throw new System.NotImplementedException();
    }

    public Task<ThemeSettingsDto> GetTenantUiCustomizationSettings(int tenantId)
    {
        throw new System.NotImplementedException();
    }

    public Task UpdateDarkModeSettingsAsync(UserIdentifier user, bool isDarkModeEnabled)
    {
        throw new System.NotImplementedException();
    }

    public Task<string> GetBodyClass()
    {
        throw new System.NotImplementedException();
    }

    public Task<string> GetBodyStyle()
    {
        throw new System.NotImplementedException();
    }
}