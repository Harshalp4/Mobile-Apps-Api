using Abp.Extensions;

namespace Bit2Sky.Bit2EHR.Configuration;

public class AzureKeyVaultConfiguration
{
    /// <summary>
    /// 
    /// </summary>
    public string TenantId { get; set; }

    public string ClientId { get; set; }

    public string ClientSecret { get; set; }

    public bool IsEnabled { get; set; }

    public string KeyVaultName { get; set; }
}

