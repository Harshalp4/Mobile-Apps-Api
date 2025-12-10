using System.Threading.Tasks;
using Abp.Application.Services;
using Bit2Sky.Bit2EHR.MultiTenancy.Payments.Dto;
using Bit2Sky.Bit2EHR.MultiTenancy.Payments.Stripe.Dto;

namespace Bit2Sky.Bit2EHR.MultiTenancy.Payments.Stripe;

public interface IStripePaymentAppService : IApplicationService
{
    Task ConfirmPayment(StripeConfirmPaymentInput input);

    StripeConfigurationDto GetConfiguration();

    Task<string> CreatePaymentSession(StripeCreatePaymentSessionInput input);
}

