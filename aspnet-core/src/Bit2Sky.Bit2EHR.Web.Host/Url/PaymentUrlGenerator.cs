using Abp.Dependency;
using Bit2Sky.Bit2EHR.MultiTenancy.Payments;
using Bit2Sky.Bit2EHR.Url;

namespace Bit2Sky.Bit2EHR.Web.Url;

public class PaymentUrlGenerator : IPaymentUrlGenerator, ITransientDependency
{
    private readonly IWebUrlService _webUrlService;

    public PaymentUrlGenerator(IWebUrlService webUrlService)
    {
        _webUrlService = webUrlService;
    }

    public string CreatePaymentRequestUrl(SubscriptionPayment subscriptionPayment)
    {
        var webSiteRootAddress = _webUrlService.GetSiteRootAddress();

        return webSiteRootAddress +
               "account/gateway-selection?paymentId=" +
               subscriptionPayment.Id;
    }
}

