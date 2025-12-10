using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Bit2Sky.Bit2EHR.ChaiCount;

/// <summary>
/// Represents a customer with loyalty points in ChaiCount
/// </summary>
[Table("ChaiCountCustomers")]
public class ChaiCountCustomer : FullAuditedEntity<Guid>, IMustHaveTenant
{
    public const int MaxClientIdLength = 64;
    public const int MaxNameLength = 128;
    public const int MaxPhoneLength = 20;
    public const int MaxNotesLength = 512;

    public int TenantId { get; set; }

    /// <summary>
    /// Client-side unique identifier for sync
    /// </summary>
    [Required]
    [StringLength(MaxClientIdLength)]
    public string ClientId { get; set; }

    /// <summary>
    /// Customer name
    /// </summary>
    [Required]
    [StringLength(MaxNameLength)]
    public string Name { get; set; }

    /// <summary>
    /// Phone number (unique identifier for the customer)
    /// </summary>
    [Required]
    [StringLength(MaxPhoneLength)]
    public string Phone { get; set; }

    /// <summary>
    /// Loyalty points accumulated
    /// </summary>
    public int LoyaltyPoints { get; set; }

    /// <summary>
    /// Total visits count
    /// </summary>
    public int TotalVisits { get; set; }

    /// <summary>
    /// Total amount spent
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalSpent { get; set; }

    /// <summary>
    /// Last visit date
    /// </summary>
    public DateTime? LastVisitDate { get; set; }

    /// <summary>
    /// Whether the customer has left a Google review
    /// </summary>
    public bool HasLeftReview { get; set; }

    /// <summary>
    /// Additional notes about the customer
    /// </summary>
    [StringLength(MaxNotesLength)]
    public string Notes { get; set; }

    /// <summary>
    /// Last sync timestamp for conflict resolution
    /// </summary>
    public DateTime LastSyncedAt { get; set; }

    protected ChaiCountCustomer()
    {
    }

    public ChaiCountCustomer(
        Guid id,
        int tenantId,
        string clientId,
        string name,
        string phone)
    {
        Id = id;
        TenantId = tenantId;
        ClientId = clientId;
        Name = name;
        Phone = phone;
        LoyaltyPoints = 0;
        TotalVisits = 0;
        TotalSpent = 0;
        LastSyncedAt = DateTime.UtcNow;
    }

    public void AddVisit(decimal amount, int points)
    {
        TotalVisits++;
        TotalSpent += amount;
        LoyaltyPoints += points;
        LastVisitDate = DateTime.UtcNow;
    }

    public bool RedeemPoints(int points)
    {
        if (LoyaltyPoints >= points)
        {
            LoyaltyPoints -= points;
            return true;
        }
        return false;
    }
}
