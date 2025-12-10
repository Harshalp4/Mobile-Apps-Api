using Bit2Sky.Bit2EHR.Dto;

namespace Bit2Sky.Bit2EHR.WebHooks.Dto;

public class GetAllSendAttemptsInput : PagedInputDto
{
    public string SubscriptionId { get; set; }
}

