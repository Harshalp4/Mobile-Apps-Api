using System.Threading.Tasks;
using Abp.Application.Services;
using Bit2Sky.Bit2EHR.Install.Dto;

namespace Bit2Sky.Bit2EHR.Install;

public interface IInstallAppService : IApplicationService
{
    Task Setup(InstallDto input);

    AppSettingsJsonDto GetAppSettingsJson();

    CheckDatabaseOutput CheckDatabase();
}
