using Bit2Sky.Bit2EHR.Dto;

namespace Bit2Sky.Bit2EHR.Organizations.Dto;

public class FindOrganizationUnitRolesInput : PagedAndFilteredInputDto
{
    public long OrganizationUnitId { get; set; }
}

