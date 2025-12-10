using Abp.Events.Bus.Handlers;
using Bit2Sky.Bit2EHR.MultiTenancy.Subscription;

namespace Bit2Sky.Bit2EHR.MultiTenancy.Payments;

public interface ISupportsRecurringPayments :
    IEventHandler<RecurringPaymentsDisabledEventData>,
    IEventHandler<RecurringPaymentsEnabledEventData>,
    IEventHandler<SubscriptionUpdatedEventData>,
    IEventHandler<SubscriptionCancelledEventData>
{

}

