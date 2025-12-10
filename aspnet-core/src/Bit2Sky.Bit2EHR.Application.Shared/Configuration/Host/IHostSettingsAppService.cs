using System.Threading.Tasks;
using Abp.Application.Services;
using Bit2Sky.Bit2EHR.Configuration.Host.Dto;

namespace Bit2Sky.Bit2EHR.Configuration.Host;

public interface IHostSettingsAppService : IApplicationService
{
    Task<HostSettingsEditDto> GetAllSettings();

    Task UpdateAllSettings(HostSettingsEditDto input);

    Task SendTestEmail(SendTestEmailInput input);
}

