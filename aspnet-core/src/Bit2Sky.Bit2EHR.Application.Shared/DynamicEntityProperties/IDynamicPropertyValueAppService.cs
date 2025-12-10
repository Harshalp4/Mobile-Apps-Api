using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Bit2Sky.Bit2EHR.DynamicEntityProperties.Dto;

namespace Bit2Sky.Bit2EHR.DynamicEntityProperties;

public interface IDynamicPropertyValueAppService
{
    Task<DynamicPropertyValueDto> Get(int id);

    Task<ListResultDto<DynamicPropertyValueDto>> GetAllValuesOfDynamicProperty(EntityDto input);

    Task Add(DynamicPropertyValueDto dto);

    Task Update(DynamicPropertyValueDto dto);

    Task Delete(int id);
}

