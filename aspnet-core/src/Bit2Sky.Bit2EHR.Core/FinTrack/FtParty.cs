using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Bit2Sky.Bit2EHR.FinTrack;

/// <summary>
/// Represents a party (person/business) for money tracking in FinTrack
/// </summary>
[Table("FtParties")]
public class FtParty : FullAuditedEntity<Guid>, IMustHaveTenant
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
    /// Party name
    /// </summary>
    [Required]
    [StringLength(MaxNameLength)]
    public string Name { get; set; }

    /// <summary>
    /// Phone number
    /// </summary>
    [StringLength(MaxPhoneLength)]
    public string Phone { get; set; }

    /// <summary>
    /// Amount the party owes you (receivables)
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal OwesYou { get; set; }

    /// <summary>
    /// Amount you owe the party (payables)
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal YouOwe { get; set; }

    /// <summary>
    /// Net balance (OwesYou - YouOwe, positive = they owe you)
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal NetBalance { get; set; }

    /// <summary>
    /// Additional notes
    /// </summary>
    [StringLength(MaxNotesLength)]
    public string Notes { get; set; }

    /// <summary>
    /// Last sync timestamp for conflict resolution
    /// </summary>
    public DateTime LastSyncedAt { get; set; }

    protected FtParty()
    {
    }

    public FtParty(
        Guid id,
        int tenantId,
        string clientId,
        string name,
        string phone = null)
    {
        Id = id;
        TenantId = tenantId;
        ClientId = clientId;
        Name = name;
        Phone = phone;
        OwesYou = 0;
        YouOwe = 0;
        NetBalance = 0;
        LastSyncedAt = DateTime.UtcNow;
    }

    public void UpdateBalance(decimal owesYou, decimal youOwe)
    {
        OwesYou = owesYou;
        YouOwe = youOwe;
        NetBalance = owesYou - youOwe;
    }
}
