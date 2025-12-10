using Abp.Application.Services.Dto;

namespace Bit2Sky.Bit2EHR.Notifications.Dto;

public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
{
    public string Filter { get; set; }
}

