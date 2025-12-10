using System;
using System.Collections.Generic;

namespace Bit2Sky.Bit2EHR.ChaiCount.Dto;

/// <summary>
/// Input for deleting records
/// </summary>
public class DeleteRecordsInput
{
    /// <summary>
    /// Type of records to delete: items, sales, customers, inventory, offers
    /// </summary>
    public string RecordType { get; set; }

    /// <summary>
    /// List of client IDs to delete
    /// </summary>
    public List<string> ClientIds { get; set; }
}

/// <summary>
/// Result of delete operation
/// </summary>
public class DeleteRecordsResult
{
    public bool Success { get; set; }
    public int DeletedCount { get; set; }
    public List<string> DeletedClientIds { get; set; }
    public List<string> FailedClientIds { get; set; }
    public string ErrorMessage { get; set; }
}

/// <summary>
/// Input for deleting all data (factory reset)
/// </summary>
public class DeleteAllDataInput
{
    /// <summary>
    /// Confirmation token - must be "CONFIRM_DELETE_ALL"
    /// </summary>
    public string ConfirmationToken { get; set; }

    /// <summary>
    /// Keep user account but delete all ChaiCount data
    /// </summary>
    public bool KeepAccount { get; set; } = true;
}
