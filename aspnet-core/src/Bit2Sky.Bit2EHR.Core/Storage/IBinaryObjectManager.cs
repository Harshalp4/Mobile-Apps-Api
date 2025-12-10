using System;
using System.Threading.Tasks;

namespace Bit2Sky.Bit2EHR.Storage;

public interface IBinaryObjectManager
{
    Task<BinaryObject> GetOrNullAsync(Guid id);

    Task SaveAsync(BinaryObject file);

    Task DeleteAsync(Guid id);
}

