using Bit2Sky.Bit2EHR.Auditing.Dto;
using Bit2Sky.Bit2EHR.Dto;
using Bit2Sky.Bit2EHR.EntityChanges.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bit2Sky.Bit2EHR.Auditing.Exporting;

public interface IAuditLogListExcelExporter
{
    Task<FileDto> ExportToFile(List<AuditLogListDto> auditLogListDtos);

    Task<FileDto> ExportToFile(List<EntityChangeListDto> entityChangeListDtos);
}
