using System.Collections.Generic;
using System.IO;

namespace Bit2Sky.Bit2EHR.Storage.FileValidator;
public interface IFileValidateInput
{
    string FileName { get; }
    string ContentType { get; }
    long Length { get; }
    Stream OpenReadStream();
}

