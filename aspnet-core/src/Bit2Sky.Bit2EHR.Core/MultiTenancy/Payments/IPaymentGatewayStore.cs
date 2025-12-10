using System.Collections.Generic;

namespace Bit2Sky.Bit2EHR.MultiTenancy.Payments;

public interface IPaymentGatewayStore
{
    List<PaymentGatewayModel> GetActiveGateways();
}

