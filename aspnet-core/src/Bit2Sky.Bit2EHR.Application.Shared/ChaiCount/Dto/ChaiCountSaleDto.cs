using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;

namespace Bit2Sky.Bit2EHR.ChaiCount.Dto;

public class ChaiCountSaleDto : EntityDto<Guid>
{
    public string ClientId { get; set; }
    public DateTime SaleDate { get; set; }
    public decimal TotalAmount { get; set; }
    public int TotalItems { get; set; }
    public Guid? CustomerId { get; set; }
    public string CustomerName { get; set; }
    public string Notes { get; set; }
    public bool IsDayClosed { get; set; }
    public DateTime LastSyncedAt { get; set; }
    public DateTime CreationTime { get; set; }
    public List<ChaiCountSaleItemDto> SaleItems { get; set; }
}

public class ChaiCountSaleItemDto : EntityDto<Guid>
{
    public string ClientId { get; set; }
    public Guid SaleId { get; set; }
    public Guid ItemId { get; set; }
    public string ItemName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalAmount { get; set; }
}

public class CreateChaiCountSaleDto
{
    public string ClientId { get; set; }
    public DateTime SaleDate { get; set; }
    public decimal TotalAmount { get; set; }
    public int TotalItems { get; set; }
    public Guid? CustomerId { get; set; }
    public string Notes { get; set; }
    public bool IsDayClosed { get; set; }
    public DateTime LastSyncedAt { get; set; }
    public List<CreateChaiCountSaleItemDto> SaleItems { get; set; }
}

public class CreateChaiCountSaleItemDto
{
    public string ClientId { get; set; }
    public string ItemClientId { get; set; }
    public string ItemName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
