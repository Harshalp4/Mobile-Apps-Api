using Abp.Web.Models.AbpUserConfiguration;

namespace Bit2Sky.Bit2EHR.Configuration;

public static class AbpUserConfigurationDtoExtensions
{
    public static bool HasSessionUserId(this AbpUserConfigurationDto userConfiguration)
    {
        return userConfiguration.Session?.UserId != null;
    }
}

