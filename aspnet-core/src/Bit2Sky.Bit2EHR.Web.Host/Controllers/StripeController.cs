using Bit2Sky.Bit2EHR.MultiTenancy.Payments.Stripe;

namespace Bit2Sky.Bit2EHR.Web.Controllers;

public class StripeController : StripeControllerBase
{
    public StripeController(
        StripeGatewayManager stripeGatewayManager,
        StripePaymentGatewayConfiguration stripeConfiguration,
        IStripePaymentAppService stripePaymentAppService)
        : base(stripeGatewayManager, stripeConfiguration, stripePaymentAppService)
    {
    }
}

