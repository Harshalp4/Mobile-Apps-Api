using Abp.Dependency;

namespace Bit2Sky.Bit2EHR.MultiTenancy.Payments;

public interface IPaymentGatewayConfiguration : ITransientDependency
{
    bool IsActive { get; }

    bool SupportsRecurringPayments { get; }

    SubscriptionPaymentGatewayType GatewayType { get; }
}

