using Microsoft.AspNetCore.Components;
using Bit2Sky.Bit2EHR.Authorization.Accounts;
using Bit2Sky.Bit2EHR.Authorization.Accounts.Dto;
using Bit2Sky.Bit2EHR.Maui.Core.Components;
using Bit2Sky.Bit2EHR.Maui.Core.Threading;
using Bit2Sky.Bit2EHR.Maui.Models.Login;

namespace Bit2Sky.Bit2EHR.Maui.Pages.Login;

public partial class EmailActivationModal : ModalBase
{
    public override string ModalId => "email-activation-modal";

    [Parameter] public EventCallback OnSave { get; set; }

    public EmailActivationModel emailActivationModel { get; set; } = new EmailActivationModel();

    private readonly IAccountAppService _accountAppService;

    public EmailActivationModal()
    {
        _accountAppService = Resolve<IAccountAppService>();
    }

    protected virtual async Task Save()
    {
        await SetBusyAsync(async () =>
        {
            await WebRequestExecuter.Execute(
                async () =>
                    await _accountAppService.SendEmailActivationLink(new SendEmailActivationLinkInput
                    {
                        EmailAddress = emailActivationModel.EmailAddress
                    }),
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