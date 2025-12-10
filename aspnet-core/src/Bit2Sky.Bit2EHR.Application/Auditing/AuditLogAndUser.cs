using Abp.Auditing;
using Bit2Sky.Bit2EHR.Authorization.Users;

namespace Bit2Sky.Bit2EHR.Auditing;

/// <summary>
/// A helper class to store an <see cref="AuditLog"/> and a <see cref="User"/> object.
/// </summary>
public class AuditLogAndUser
{
    public AuditLog AuditLog { get; set; }

    public User User { get; set; }
}
