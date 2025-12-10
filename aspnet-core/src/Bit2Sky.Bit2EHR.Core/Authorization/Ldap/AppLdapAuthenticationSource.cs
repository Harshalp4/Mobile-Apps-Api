using Abp.Zero.Ldap.Authentication;
using Abp.Zero.Ldap.Configuration;
using Bit2Sky.Bit2EHR.Authorization.Users;
using Bit2Sky.Bit2EHR.MultiTenancy;

namespace Bit2Sky.Bit2EHR.Authorization.Ldap;

public class AppLdapAuthenticationSource : LdapAuthenticationSource<Tenant, User>
{
    public AppLdapAuthenticationSource(ILdapSettings settings, IAbpZeroLdapModuleConfig ldapModuleConfig)
        : base(settings, ldapModuleConfig)
    {
    }
}

