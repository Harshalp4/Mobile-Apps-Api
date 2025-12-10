using System.Threading.Tasks;
using Abp.Application.Services;
using Bit2Sky.Bit2EHR.MultiTenancy.Dto;
using Bit2Sky.Bit2EHR.MultiTenancy.Payments.Dto;

namespace Bit2Sky.Bit2EHR.MultiTenancy;

public interface ISubscriptionAppService : IApplicationService
{
    Task DisableRecurringPayments();

    Task EnableRecurringPayments();

    Task<long> StartExtendSubscription(StartExtendSubscriptionInput input);

    Task<StartUpgradeSubscriptionOutput> StartUpgradeSubscription(StartUpgradeSubscriptionInput input);

    Task<long> StartTrialToBuySubscription(StartTrialToBuySubscriptionInput input);
}

