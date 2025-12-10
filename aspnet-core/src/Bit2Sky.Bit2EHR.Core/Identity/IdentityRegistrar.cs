using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Bit2Sky.Bit2EHR.Authentication.TwoFactor.Google;
using Bit2Sky.Bit2EHR.Authorization;
using Bit2Sky.Bit2EHR.Authorization.Roles;
using Bit2Sky.Bit2EHR.Authorization.Users;
using Bit2Sky.Bit2EHR.Editions;
using Bit2Sky.Bit2EHR.MultiTenancy;

namespace Bit2Sky.Bit2EHR.Identity;

public static class IdentityRegistrar
{
    public static IdentityBuilder Register(IServiceCollection services)
    {
        services.AddLogging();

        return services.AddAbpIdentity<Tenant, User, Role>(options =>
            {
                options.Tokens.ProviderMap[GoogleAuthenticatorProvider.Name] = new TokenProviderDescriptor(typeof(GoogleAuthenticatorProvider));
            })
            .AddAbpTenantManager<TenantManager>()
            .AddAbpUserManager<UserManager>()
            .AddAbpRoleManager<RoleManager>()
            .AddAbpEditionManager<EditionManager>()
            .AddAbpUserStore<UserStore>()
            .AddAbpRoleStore<RoleStore>()
            .AddAbpSignInManager<SignInManager>()
            .AddAbpUserClaimsPrincipalFactory<UserClaimsPrincipalFactory>()
            .AddAbpSecurityStampValidator<SecurityStampValidator>()
            .AddPermissionChecker<PermissionChecker>()
            .AddDefaultTokenProviders();
    }
}

