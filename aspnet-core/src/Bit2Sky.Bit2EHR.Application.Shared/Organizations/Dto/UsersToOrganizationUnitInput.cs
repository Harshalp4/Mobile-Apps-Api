using System.ComponentModel.DataAnnotations;

namespace Bit2Sky.Bit2EHR.Organizations.Dto;

public class UsersToOrganizationUnitInput
{
    public long[] UserIds { get; set; }

    [Range(1, long.MaxValue)]
    public long OrganizationUnitId { get; set; }
}

