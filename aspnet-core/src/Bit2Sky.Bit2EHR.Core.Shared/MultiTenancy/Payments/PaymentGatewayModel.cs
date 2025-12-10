namespace Bit2Sky.Bit2EHR.MultiTenancy.Payments;

public class PaymentGatewayModel
{
    public SubscriptionPaymentGatewayType GatewayType { get; set; }

    public bool SupportsRecurringPayments { get; set; }
}

