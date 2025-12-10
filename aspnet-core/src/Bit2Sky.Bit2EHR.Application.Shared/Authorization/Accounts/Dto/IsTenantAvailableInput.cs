using System.ComponentModel.DataAnnotations;
using Abp.MultiTenancy;

namespace Bit2Sky.Bit2EHR.Authorization.Accounts.Dto;

public class IsTenantAvailableInput
{
    [Required]
    [MaxLength(AbpTenantBase.MaxTenancyNameLength)]
    public string TenancyName { get; set; }
}

