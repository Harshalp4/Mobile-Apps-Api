using Abp.Auditing;
using Bit2Sky.Bit2EHR.Configuration.Dto;

namespace Bit2Sky.Bit2EHR.Configuration.Tenants.Dto;

public class TenantEmailSettingsEditDto : EmailSettingsEditDto
{
    public bool UseHostDefaultEmailSettings { get; set; }
}

