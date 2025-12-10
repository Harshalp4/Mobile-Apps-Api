using System.Threading.Tasks;
using Abp.Application.Services;
using Bit2Sky.Bit2EHR.Editions.Dto;
using Bit2Sky.Bit2EHR.MultiTenancy.Dto;

namespace Bit2Sky.Bit2EHR.MultiTenancy;

public interface ITenantRegistrationAppService : IApplicationService
{
    Task<RegisterTenantOutput> RegisterTenant(RegisterTenantInput input);

    Task<EditionsSelectOutput> GetEditionsForSelect();

    Task<EditionSelectDto> GetEdition(int editionId);

    Task BuyNowSucceed(long paymentId);

    Task NewRegistrationSucceed(long paymentId);

    Task UpgradeSucceed(long paymentId);

    Task ExtendSucceed(long paymentId);
}

