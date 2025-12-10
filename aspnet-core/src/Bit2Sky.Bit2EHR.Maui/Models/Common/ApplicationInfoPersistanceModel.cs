using Abp.AutoMapper;
using Bit2Sky.Bit2EHR.Sessions.Dto;

namespace Bit2Sky.Bit2EHR.Maui.Models.Common;

[AutoMapFrom(typeof(ApplicationInfoDto)),
 AutoMapTo(typeof(ApplicationInfoDto))]
public class ApplicationInfoPersistanceModel
{
    public string Version { get; set; }

    public DateTime ReleaseDate { get; set; }

    public bool IsQrLoginEnabled { get; set; }
}