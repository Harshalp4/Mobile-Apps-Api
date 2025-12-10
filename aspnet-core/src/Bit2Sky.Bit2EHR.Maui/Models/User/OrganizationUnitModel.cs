using Abp.AutoMapper;
using Bit2Sky.Bit2EHR.Organizations.Dto;

namespace Bit2Sky.Bit2EHR.Maui.Models.User;

[AutoMapFrom(typeof(OrganizationUnitDto))]
public class OrganizationUnitModel : OrganizationUnitDto
{
    public bool IsAssigned { get; set; }
}