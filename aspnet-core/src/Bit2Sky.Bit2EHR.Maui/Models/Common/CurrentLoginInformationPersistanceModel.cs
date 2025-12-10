using Abp.AutoMapper;
using Bit2Sky.Bit2EHR.Sessions.Dto;

namespace Bit2Sky.Bit2EHR.Maui.Models.Common;

[AutoMapFrom(typeof(GetCurrentLoginInformationsOutput)),
 AutoMapTo(typeof(GetCurrentLoginInformationsOutput))]
public class CurrentLoginInformationPersistanceModel
{
    public UserLoginInfoPersistanceModel User { get; set; }

    public TenantLoginInfoPersistanceModel Tenant { get; set; }

    public ApplicationInfoPersistanceModel Application { get; set; }
}