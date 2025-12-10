using System.ComponentModel.DataAnnotations;

namespace Bit2Sky.Bit2EHR.DynamicEntityPropertyValues.Dto;

public class GetAllDynamicEntityPropertyValuesInput
{
    [Required]
    public string EntityFullName { get; set; }

    [Required]
    public string EntityId { get; set; }
}

