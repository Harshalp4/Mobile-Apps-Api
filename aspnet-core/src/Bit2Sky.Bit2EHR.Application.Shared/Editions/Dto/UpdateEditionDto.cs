using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace Bit2Sky.Bit2EHR.Editions.Dto;

public class UpdateEditionDto
{
    [Required]
    public EditionEditDto Edition { get; set; }

    [Required]
    public List<NameValueDto> FeatureValues { get; set; }
}

