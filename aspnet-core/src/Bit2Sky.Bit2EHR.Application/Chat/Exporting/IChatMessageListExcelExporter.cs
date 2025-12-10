using System.Collections.Generic;
using System.Threading.Tasks;
using Abp;
using Bit2Sky.Bit2EHR.Chat.Dto;
using Bit2Sky.Bit2EHR.Dto;

namespace Bit2Sky.Bit2EHR.Chat.Exporting;

public interface IChatMessageListExcelExporter
{
    Task<FileDto> ExportToFile(UserIdentifier user, List<ChatMessageExportDto> messages);
}