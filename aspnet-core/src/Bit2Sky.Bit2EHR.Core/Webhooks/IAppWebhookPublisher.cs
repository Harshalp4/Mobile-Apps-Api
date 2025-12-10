using System.Threading.Tasks;
using Bit2Sky.Bit2EHR.Authorization.Users;

namespace Bit2Sky.Bit2EHR.WebHooks;

public interface IAppWebhookPublisher
{
    Task PublishTestWebhook();
}

