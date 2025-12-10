using Microsoft.Extensions.Configuration;

namespace Bit2Sky.Bit2EHR.Configuration;

public interface IAppConfigurationAccessor
{
    IConfigurationRoot Configuration { get; }
}

