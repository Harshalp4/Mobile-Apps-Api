using Bit2Sky.Bit2EHR.ApiClient;
using Bit2Sky.Bit2EHR.ApiClient.Models;
using Bit2Sky.Bit2EHR.Sessions.Dto;

namespace Bit2Sky.Bit2EHR.Maui.Services.Storage;

public interface IDataStorageService
{
    Task StoreAccessTokenAsync(string newAccessToken, string newEncryptedAccessToken);

    Task StoreAuthenticateResultAsync(AbpAuthenticateResultModel authenticateResultModel);

    AbpAuthenticateResultModel RetrieveAuthenticateResult();

    TenantInformation RetrieveTenantInfo();

    GetCurrentLoginInformationsOutput RetrieveLoginInfo();

    void ClearSessionPersistance();

    Task StoreLoginInformationAsync(GetCurrentLoginInformationsOutput loginInfo);

    Task StoreTenantInfoAsync(TenantInformation tenantInfo);
}