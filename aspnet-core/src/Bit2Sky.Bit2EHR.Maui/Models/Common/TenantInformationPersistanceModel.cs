using Abp.AutoMapper;
using Bit2Sky.Bit2EHR.ApiClient;

namespace Bit2Sky.Bit2EHR.Maui.Models.Common;

[AutoMapFrom(typeof(TenantInformation)),
 AutoMapTo(typeof(TenantInformation))]
public class TenantInformationPersistanceModel
{
    public string TenancyName { get; set; }

    public int TenantId { get; set; }
}