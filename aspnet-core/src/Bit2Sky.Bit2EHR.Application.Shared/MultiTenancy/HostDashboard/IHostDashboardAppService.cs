using System.Threading.Tasks;
using Abp.Application.Services;
using Bit2Sky.Bit2EHR.MultiTenancy.HostDashboard.Dto;

namespace Bit2Sky.Bit2EHR.MultiTenancy.HostDashboard;

public interface IHostDashboardAppService : IApplicationService
{
    Task<TopStatsData> GetTopStatsData(GetTopStatsInput input);

    Task<GetRecentTenantsOutput> GetRecentTenantsData();

    Task<GetExpiringTenantsOutput> GetSubscriptionExpiringTenantsData();

    Task<GetIncomeStatisticsDataOutput> GetIncomeStatistics(GetIncomeStatisticsDataInput input);

    Task<GetEditionTenantStatisticsOutput> GetEditionTenantStatistics(GetEditionTenantStatisticsInput input);
}

