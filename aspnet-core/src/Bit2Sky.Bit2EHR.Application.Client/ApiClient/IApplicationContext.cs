using Abp.Localization;
using Abp.Web.Models.AbpUserConfiguration;
using JetBrains.Annotations;
using Bit2Sky.Bit2EHR.Sessions.Dto;

namespace Bit2Sky.Bit2EHR.ApiClient;

public interface IApplicationContext
{
    [CanBeNull]
    TenantInformation CurrentTenant { get; }

    AbpUserConfigurationDto Configuration { get; set; }

    GetCurrentLoginInformationsOutput LoginInfo { get; }

    void ClearLoginInfo();

    void SetLoginInfo(GetCurrentLoginInformationsOutput loginInfo);

    void SetAsHost();

    void SetAsTenant(string tenancyName, int tenantId);

    LanguageInfo CurrentLanguage { get; set; }

    void Load(TenantInformation currentTenant, GetCurrentLoginInformationsOutput loginInfo);
}

