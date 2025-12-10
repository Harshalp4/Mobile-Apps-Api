using Bit2Sky.Bit2EHR.MultiTenancy.Payments;

namespace Bit2Sky.Bit2EHR.MultiTenancy.Dto;

public class StartTrialToBuySubscriptionInput
{
    public PaymentPeriodType PaymentPeriodType { get; set; }

    public string SuccessUrl { get; set; }

    public string ErrorUrl { get; set; }
}

