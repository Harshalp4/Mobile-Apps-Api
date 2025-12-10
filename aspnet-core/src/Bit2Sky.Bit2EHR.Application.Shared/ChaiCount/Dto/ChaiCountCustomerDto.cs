using System;
using Abp.Application.Services.Dto;

namespace Bit2Sky.Bit2EHR.ChaiCount.Dto;

public class ChaiCountCustomerDto : EntityDto<Guid>
{
    public string ClientId { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public int LoyaltyPoints { get; set; }
    public int TotalVisits { get; set; }
    public decimal TotalSpent { get; set; }
    public DateTime? LastVisitDate { get; set; }
    public bool HasLeftReview { get; set; }
    public string Notes { get; set; }
    public DateTime LastSyncedAt { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
}

public class CreateOrUpdateChaiCountCustomerDto
{
    public string ClientId { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public int LoyaltyPoints { get; set; }
    public int TotalVisits { get; set; }
    public decimal TotalSpent { get; set; }
    public DateTime? LastVisitDate { get; set; }
    public bool HasLeftReview { get; set; }
    public string Notes { get; set; }
    public DateTime LastSyncedAt { get; set; }
}
