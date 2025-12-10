using Abp.EntityHistory;
using Bit2Sky.Bit2EHR.Authorization.Users;
using System.Collections.Generic;

namespace Bit2Sky.Bit2EHR.EntityChanges;

public class EntityChangePropertyAndUser
{
    public EntityChange EntityChange { get; set; }
    public EntityChangeSet EntityChangeSet { get; set; }
    public List<EntityPropertyChange> PropertyChanges { get; set; }
    public User User { get; set; }
    public string ImpersonatorUserName { get; set; }
}
