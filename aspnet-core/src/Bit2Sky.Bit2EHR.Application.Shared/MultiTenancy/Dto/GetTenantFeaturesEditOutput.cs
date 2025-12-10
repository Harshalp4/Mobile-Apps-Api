using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Bit2Sky.Bit2EHR.Editions.Dto;

namespace Bit2Sky.Bit2EHR.MultiTenancy.Dto;

public class GetTenantFeaturesEditOutput
{
    public List<NameValueDto> FeatureValues { get; set; }

    public List<FlatFeatureDto> Features { get; set; }
}

