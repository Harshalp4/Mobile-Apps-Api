using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace Bit2Sky.Bit2EHR.Dto;

public class PagedInputDto : IPagedResultRequest
{
    [Range(1, AppConsts.MaxPageSize)]
    public int MaxResultCount { get; set; }

    [Range(0, int.MaxValue)]
    public int SkipCount { get; set; }

    public PagedInputDto()
    {
        MaxResultCount = AppConsts.DefaultPageSize;
    }
}

