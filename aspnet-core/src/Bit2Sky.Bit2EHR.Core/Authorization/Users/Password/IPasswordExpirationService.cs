using Abp.Domain.Services;

namespace Bit2Sky.Bit2EHR.Authorization.Users.Password;

public interface IPasswordExpirationService : IDomainService
{
    void ForcePasswordExpiredUsersToChangeTheirPassword();
}

