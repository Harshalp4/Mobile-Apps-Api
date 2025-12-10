using Abp;
using Abp.Domain.Services;
using System.Threading.Tasks;

namespace Bit2Sky.Bit2EHR.Authorization.Users.Profile;

public interface IProfileImageService : IDomainService
{
    Task<string> GetProfilePictureContentForUser(UserIdentifier userIdentifier);
}

