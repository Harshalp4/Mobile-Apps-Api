using System.Threading.Tasks;
using Abp.Domain.Policies;

namespace Bit2Sky.Bit2EHR.Authorization.Users;

public interface IUserPolicy : IPolicy
{
    Task CheckMaxUserCountAsync(int tenantId);
}

