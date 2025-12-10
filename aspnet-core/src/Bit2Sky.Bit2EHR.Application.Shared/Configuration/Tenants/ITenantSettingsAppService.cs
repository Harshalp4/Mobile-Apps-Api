using System.Threading.Tasks;
using Abp.Application.Services;
using Bit2Sky.Bit2EHR.Configuration.Tenants.Dto;

namespace Bit2Sky.Bit2EHR.Configuration.Tenants;

public interface ITenantSettingsAppService : IApplicationService
{
    Task<TenantSettingsEditDto> GetAllSettings();

    Task UpdateAllSettings(TenantSettingsEditDto input);

    Task ClearDarkLogo();

    Task ClearDarkLogoMinimal();

    Task ClearLightLogo();

    Task ClearLightLogoMinimal();

    Task ClearCustomCss();
}

