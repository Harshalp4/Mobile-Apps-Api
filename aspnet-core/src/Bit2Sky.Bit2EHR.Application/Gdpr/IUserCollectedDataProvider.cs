using System.Collections.Generic;
using System.Threading.Tasks;
using Abp;
using Bit2Sky.Bit2EHR.Dto;

namespace Bit2Sky.Bit2EHR.Gdpr;

public interface IUserCollectedDataProvider
{
    Task<List<FileDto>> GetFiles(UserIdentifier user);
}
