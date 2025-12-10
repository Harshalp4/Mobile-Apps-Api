using Bit2Sky.Bit2EHR.ApiClient.Models;

namespace Bit2Sky.Bit2EHR.Maui.Services.Account;

public interface IAccountService
{
    AbpAuthenticateModel AbpAuthenticateModel { get; set; }

    AbpAuthenticateResultModel AuthenticateResultModel { get; set; }

    Task LoginUserAsync();

    Task LogoutAsync();
}