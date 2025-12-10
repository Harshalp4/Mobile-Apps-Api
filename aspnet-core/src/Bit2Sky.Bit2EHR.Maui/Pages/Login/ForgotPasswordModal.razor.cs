using Microsoft.AspNetCore.Components;
using Bit2Sky.Bit2EHR.Authorization.Accounts;
using Bit2Sky.Bit2EHR.Authorization.Accounts.Dto;
using Bit2Sky.Bit2EHR.Maui.Core.Components;
using Bit2Sky.Bit2EHR.Maui.Core.Threading;
using Bit2Sky.Bit2EHR.Maui.Models.Login;

namespace Bit2Sky.Bit2EHR.Maui.Pages.Login;

public partial class ForgotPasswordModal : ModalBase
{
    public override string ModalId => "forgot-password-modal";

    [Parameter] public EventCallback OnSave { get; set; }

    public ForgotPasswordModel ForgotPasswordModel { get; } = new();

    private readonly IAccountAppService _accountAppService;

    public ForgotPasswordModal()
    {
        _accountAppService = Resolve<IAccountAppService>();
    }

    protected virtual async Task Save()
    {
        await SetBusyAsync(async () =>
        {
            await WebRequestExecuter.Execute(
                async () =>
                    await _accountAppService.SendPasswordResetCode(new SendPasswordResetCodeInput { EmailAddress = ForgotPasswordModel.EmailAddress }),
                async () =>
                {
                    await OnSave.InvokeAsync();
                }
            );
        });
    }

    protected virtual async Task Cancel()
    {
        await Hide();
    }
}