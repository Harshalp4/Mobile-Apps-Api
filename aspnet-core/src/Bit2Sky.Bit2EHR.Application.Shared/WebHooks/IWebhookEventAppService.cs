using System.Threading.Tasks;
using Abp.Webhooks;

namespace Bit2Sky.Bit2EHR.WebHooks;

public interface IWebhookEventAppService
{
    Task<WebhookEvent> Get(string id);
}

