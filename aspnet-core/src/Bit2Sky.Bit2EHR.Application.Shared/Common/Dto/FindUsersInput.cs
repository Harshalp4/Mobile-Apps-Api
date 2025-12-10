using Bit2Sky.Bit2EHR.Dto;

namespace Bit2Sky.Bit2EHR.Common.Dto;

public class FindUsersInput : PagedAndFilteredInputDto
{
    public int? TenantId { get; set; }

    public bool ExcludeCurrentUser { get; set; }
}

