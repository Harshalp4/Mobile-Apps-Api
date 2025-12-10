using System.Collections.Generic;
using Abp.Application.Services.Dto;

namespace Bit2Sky.Bit2EHR.Localization.Dto;

public class GetLanguagesOutput : ListResultDto<ApplicationLanguageListDto>
{
    public string DefaultLanguageName { get; set; }

    public GetLanguagesOutput()
    {

    }

    public GetLanguagesOutput(IReadOnlyList<ApplicationLanguageListDto> items, string defaultLanguageName)
        : base(items)
    {
        DefaultLanguageName = defaultLanguageName;
    }
}

