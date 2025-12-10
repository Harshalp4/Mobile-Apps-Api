using GraphQL.Types;
using Bit2Sky.Bit2EHR.Dto;

namespace Bit2Sky.Bit2EHR.Types;

public class OrganizationUnitType : ObjectGraphType<OrganizationUnitDto>
{
    public OrganizationUnitType()
    {
        Name = "OrganizationUnitType";

        Field(x => x.Id);
        Field(x => x.Code);
        Field(x => x.DisplayName);
        Field(x => x.TenantId, nullable: true);
    }
}

