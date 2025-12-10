using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Bit2Sky.Bit2EHR.Sessions.Dto;

namespace Bit2Sky.Bit2EHR.Maui.Models.Common;

[AutoMapFrom(typeof(TenantLoginInfoDto)),
 AutoMapTo(typeof(TenantLoginInfoDto))]
public class TenantLoginInfoPersistanceModel : EntityDto
{
    public string TenancyName { get; set; }

    public string Name { get; set; }
}