using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.BackgroundJobs;
using Bit2Sky.Bit2EHR.Authorization;
using Bit2Sky.Bit2EHR.Authorization.Users.Importing;
using Bit2Sky.Bit2EHR.DataImporting.Excel;
using Bit2Sky.Bit2EHR.Storage;
using Bit2Sky.Bit2EHR.Storage.FileValidator;

namespace Bit2Sky.Bit2EHR.Web.Controllers;

[AbpMvcAuthorize(AppPermissions.Pages_Administration_Users)]
public class UsersController(IBinaryObjectManager binaryObjectManager, IBackgroundJobManager backgroundJobManager, IFileValidatorManager fileValidatorManager)
    : ExcelImportControllerBase(binaryObjectManager, backgroundJobManager, fileValidatorManager)
{
    public override string ImportExcelPermission => AppPermissions.Pages_Administration_Users_Create;

    public override async Task EnqueueExcelImportJobAsync(ImportFromExcelJobArgs args)
    {
        await BackgroundJobManager.EnqueueAsync<ImportUsersToExcelJob, ImportFromExcelJobArgs>(args);
    }
}