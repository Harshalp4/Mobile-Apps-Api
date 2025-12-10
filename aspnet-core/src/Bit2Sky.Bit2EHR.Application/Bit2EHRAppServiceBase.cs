using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.IdentityFramework;
using Abp.MultiTenancy;
using Abp.Runtime.Session;
using Abp.Threading;
using Microsoft.AspNetCore.Identity;
using Bit2Sky.Bit2EHR.Authorization.Users;
using Bit2Sky.Bit2EHR.MultiTenancy;

namespace Bit2Sky.Bit2EHR;

/// <summary>
/// Derive your application services from this class.
/// </summary>
public abstract class Bit2EHRAppServiceBase : ApplicationService
{
    public TenantManager TenantManager { get; set; }

    public UserManager UserManager { get; set; }

    protected Bit2EHRAppServiceBase()
    {
        LocalizationSourceName = Bit2EHRConsts.LocalizationSourceName;
    }

    protected virtual async Task<User> GetCurrentUserAsync()
    {
        var user = await UserManager.FindByIdAsync(AbpSession.GetUserId().ToString());
        if (user == null)
        {
            throw new Exception("There is no current user!");
        }

        return user;
    }

    protected virtual User GetCurrentUser()
    {
        return AsyncHelper.RunSync(GetCurrentUserAsync);
    }

    protected virtual Task<Tenant> GetCurrentTenantAsync()
    {
        using (CurrentUnitOfWork.SetTenantId(null))
        {
            return TenantManager.GetByIdAsync(AbpSession.GetTenantId());
        }
    }

    protected virtual Tenant GetCurrentTenant()
    {
        using (CurrentUnitOfWork.SetTenantId(null))
        {
            return TenantManager.GetById(AbpSession.GetTenantId());
        }
    }

    protected virtual void CheckErrors(IdentityResult identityResult)
    {
        identityResult.CheckErrors(LocalizationManager);
    }
}
