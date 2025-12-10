using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using Bit2Sky.Bit2EHR.Dto;

namespace Bit2Sky.Bit2EHR.DataImporting.Excel;

public interface IExcelInvalidEntityExporter<TEntityDto> : ITransientDependency
{
    Task<FileDto> ExportToFile(List<TEntityDto> entities);
}