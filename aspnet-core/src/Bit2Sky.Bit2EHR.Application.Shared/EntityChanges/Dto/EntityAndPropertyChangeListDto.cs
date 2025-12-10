using System.Collections.Generic;

namespace Bit2Sky.Bit2EHR.EntityChanges.Dto;

public class EntityAndPropertyChangeListDto
{
    public EntityChangeListDto EntityChange { get; set; }
    public List<EntityPropertyChangeDto> EntityPropertyChanges { get; set; }
}

