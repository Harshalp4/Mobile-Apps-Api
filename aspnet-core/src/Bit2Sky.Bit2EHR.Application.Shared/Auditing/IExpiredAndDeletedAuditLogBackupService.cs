using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Auditing;

namespace Bit2Sky.Bit2EHR.Auditing;

public interface IExpiredAndDeletedAuditLogBackupService
{
    bool CanBackup();

    Task Backup(List<AuditLog> auditLogs);
}