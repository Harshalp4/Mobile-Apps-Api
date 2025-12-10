using System;
using Abp.Application.Services.Dto;

namespace Bit2Sky.Bit2EHR.FinTrack.Dto;

public class FtCategoryDto : EntityDto<Guid>
{
    public string ClientId { get; set; }
    public string Name { get; set; }
    public string Icon { get; set; }
    public string Color { get; set; }
    public string CategoryType { get; set; }
    public bool IsSystem { get; set; }
    public int DisplayOrder { get; set; }
    public DateTime LastSyncedAt { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
}

public class CreateOrUpdateFtCategoryDto
{
    public string ClientId { get; set; }
    public string Name { get; set; }
    public string Icon { get; set; }
    public string Color { get; set; }
    public string CategoryType { get; set; }
    public bool IsSystem { get; set; }
    public int DisplayOrder { get; set; }
    public DateTime LastSyncedAt { get; set; }
}
