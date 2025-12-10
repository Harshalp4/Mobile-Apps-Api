using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Bit2Sky.Bit2EHR.Common.Dto;
using Bit2Sky.Bit2EHR.Editions.Dto;

namespace Bit2Sky.Bit2EHR.Common;

public interface ICommonLookupAppService : IApplicationService
{
    Task<ListResultDto<SubscribableEditionComboboxItemDto>> GetEditionsForCombobox(bool onlyFreeItems = false);

    Task<PagedResultDto<FindUsersOutputDto>> FindUsers(FindUsersInput input);

    GetDefaultEditionNameOutput GetDefaultEditionName();
}

