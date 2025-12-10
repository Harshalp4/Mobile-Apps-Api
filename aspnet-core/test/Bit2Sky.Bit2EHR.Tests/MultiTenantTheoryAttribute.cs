using Xunit;

namespace Bit2Sky.Bit2EHR.Tests;

public sealed class MultiTenantTheoryAttribute : TheoryAttribute
{
    private readonly bool _multiTenancyEnabled = Bit2EHRConsts.MultiTenancyEnabled;

    public MultiTenantTheoryAttribute()
    {
        if (!_multiTenancyEnabled)
        {
            Skip = "MultiTenancy is disabled.";
        }
    }
}