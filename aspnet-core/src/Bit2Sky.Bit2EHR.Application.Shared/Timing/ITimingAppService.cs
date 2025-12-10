using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Bit2Sky.Bit2EHR.Timing.Dto;

namespace Bit2Sky.Bit2EHR.Timing;

public interface ITimingAppService : IApplicationService
{
    Task<ListResultDto<NameValueDto>> GetTimezones(GetTimezonesInput input);

    Task<List<ComboboxItemDto>> GetTimezoneComboboxItems(GetTimezoneComboboxItemsInput input);
}

