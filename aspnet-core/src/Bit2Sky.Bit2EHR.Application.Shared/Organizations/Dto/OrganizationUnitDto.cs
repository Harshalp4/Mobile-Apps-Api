using Abp.Application.Services.Dto;

namespace Bit2Sky.Bit2EHR.Organizations.Dto;

public class OrganizationUnitDto : AuditedEntityDto<long>
{
    public long? ParentId { get; set; }

    public string Code { get; set; }

    public string DisplayName { get; set; }

    public int MemberCount { get; set; }

    public int RoleCount { get; set; }
}

