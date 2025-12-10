using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Bit2Sky.Bit2EHR.WebHooks.Dto;

namespace Bit2Sky.Bit2EHR.WebHooks;

public interface IWebhookAttemptAppService
{
    Task<PagedResultDto<GetAllSendAttemptsOutput>> GetAllSendAttempts(GetAllSendAttemptsInput input);

    Task<ListResultDto<GetAllSendAttemptsOfWebhookEventOutput>> GetAllSendAttemptsOfWebhookEvent(GetAllSendAttemptsOfWebhookEventInput input);
}

