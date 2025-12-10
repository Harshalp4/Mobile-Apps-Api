using Abp.Application.Services.Dto;

namespace Bit2Sky.Bit2EHR.Dto;

public class PagedAndSortedInputDto : PagedInputDto, ISortedResultRequest
{
    public string Sorting { get; set; }

    public PagedAndSortedInputDto()
    {
        MaxResultCount = AppConsts.DefaultPageSize;
    }
}

