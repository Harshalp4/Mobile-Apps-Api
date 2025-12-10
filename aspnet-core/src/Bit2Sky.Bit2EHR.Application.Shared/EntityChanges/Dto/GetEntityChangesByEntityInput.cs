using Bit2Sky.Bit2EHR.Dto;
using System;

namespace Bit2Sky.Bit2EHR.EntityChanges.Dto;

public class GetEntityChangesByEntityInput
{
    public string EntityTypeFullName { get; set; }
    public string EntityId { get; set; }
}

