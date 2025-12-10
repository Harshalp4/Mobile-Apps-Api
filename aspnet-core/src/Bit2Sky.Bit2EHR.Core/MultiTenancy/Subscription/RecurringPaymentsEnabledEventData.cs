using Abp.Events.Bus;

namespace Bit2Sky.Bit2EHR.MultiTenancy.Subscription;

public class RecurringPaymentsEnabledEventData : EventData
{
    public int TenantId { get; set; }
}

