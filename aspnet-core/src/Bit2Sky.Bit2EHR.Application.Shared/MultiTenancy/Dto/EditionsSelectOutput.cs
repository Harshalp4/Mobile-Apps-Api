using System.Collections.Generic;
using Bit2Sky.Bit2EHR.Editions.Dto;

namespace Bit2Sky.Bit2EHR.MultiTenancy.Dto;

public class EditionsSelectOutput
{
    public EditionsSelectOutput()
    {
        AllFeatures = new List<FlatFeatureSelectDto>();
        EditionsWithFeatures = new List<EditionWithFeaturesDto>();
    }

    public List<FlatFeatureSelectDto> AllFeatures { get; set; }

    public List<EditionWithFeaturesDto> EditionsWithFeatures { get; set; }
}

