using System.Threading.Tasks;
using Bit2Sky.Bit2EHR.Sessions.Dto;

namespace Bit2Sky.Bit2EHR.Web.Session;

public interface IPerRequestSessionCache
{
    Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformationsAsync();
}

