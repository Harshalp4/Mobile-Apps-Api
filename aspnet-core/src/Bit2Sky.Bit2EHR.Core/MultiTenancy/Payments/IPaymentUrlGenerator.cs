namespace Bit2Sky.Bit2EHR.MultiTenancy.Payments;

public interface IPaymentUrlGenerator
{
    string CreatePaymentRequestUrl(SubscriptionPayment subscriptionPayment);
}

