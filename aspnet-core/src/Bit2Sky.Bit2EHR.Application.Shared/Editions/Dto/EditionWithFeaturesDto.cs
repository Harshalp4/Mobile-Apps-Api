using System.Collections.Generic;
using Abp.Application.Services.Dto;

namespace Bit2Sky.Bit2EHR.Editions.Dto;

public class EditionWithFeaturesDto
{
    public EditionWithFeaturesDto()
    {
        FeatureValues = new List<NameValueDto>();
    }

    public EditionSelectDto Edition { get; set; }

    public List<NameValueDto> FeatureValues { get; set; }
}

