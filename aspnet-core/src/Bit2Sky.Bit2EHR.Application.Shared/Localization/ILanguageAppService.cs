using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Bit2Sky.Bit2EHR.Localization.Dto;

namespace Bit2Sky.Bit2EHR.Localization;

public interface ILanguageAppService : IApplicationService
{
    Task<GetLanguagesOutput> GetLanguages();

    Task<GetLanguageForEditOutput> GetLanguageForEdit(NullableIdDto input);

    Task CreateOrUpdateLanguage(CreateOrUpdateLanguageInput input);

    Task DeleteLanguage(EntityDto input);

    Task SetDefaultLanguage(SetDefaultLanguageInput input);

    Task<PagedResultDto<LanguageTextListDto>> GetLanguageTexts(GetLanguageTextsInput input);

    Task UpdateLanguageText(UpdateLanguageTextInput input);
}

