using System.Collections.Generic;
using System.Threading.Tasks;
using Bit2Sky.Bit2EHR.Authorization.Users.Dto;
using Bit2Sky.Bit2EHR.Dto;

namespace Bit2Sky.Bit2EHR.Authorization.Users.Exporting;

public interface IUserListExcelExporter
{
    Task<FileDto> ExportToFile(List<UserListDto> userListDtos, List<string> selectedColumns);
}