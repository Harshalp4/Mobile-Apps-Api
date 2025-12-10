using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bit2Sky.Bit2EHR.MultiTenancy.HostDashboard.Dto;

namespace Bit2Sky.Bit2EHR.MultiTenancy.HostDashboard;

public interface IIncomeStatisticsService
{
    Task<List<IncomeStastistic>> GetIncomeStatisticsData(DateTime startDate, DateTime endDate,
        ChartDateInterval dateInterval);
}
