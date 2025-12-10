using Abp.Application.Services;
using Bit2Sky.Bit2EHR.Dto;
using Bit2Sky.Bit2EHR.Logging.Dto;

namespace Bit2Sky.Bit2EHR.Logging;

public interface IWebLogAppService : IApplicationService
{
    GetLatestWebLogsOutput GetLatestWebLogs();

    FileDto DownloadWebLogs();
}

