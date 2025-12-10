using Xunit;

namespace Bit2Sky.Bit2EHR.Tests;

public sealed class MultiTenantFactAttribute : FactAttribute
{
    private readonly bool _multiTenancyEnabled = Bit2EHRConsts.MultiTenancyEnabled;

    public MultiTenantFactAttribute()
    {
        if (!_multiTenancyEnabled)
        {
            Skip = "MultiTenancy is disabled.";
        }
    }
}
