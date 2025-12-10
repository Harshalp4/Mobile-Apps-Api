using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Bit2Sky.Bit2EHR.EntityChanges.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bit2Sky.Bit2EHR.EntityChanges;

public interface IEntityChangeAppService : IApplicationService
{
    Task<ListResultDto<EntityAndPropertyChangeListDto>> GetEntityChangesByEntity(GetEntityChangesByEntityInput input);
}

