using Abp;
using System.Threading.Tasks;

namespace Bit2Sky.Bit2EHR.Authorization.Users.DataCleaners;

public interface IUserDataCleaner
{
    Task CleanUserData(UserIdentifier userIdentifier);
}

