using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Bit2Sky.Bit2EHR.MultiTenancy.Accounting.Dto;

namespace Bit2Sky.Bit2EHR.MultiTenancy.Accounting;

public interface IInvoiceAppService
{
    Task<InvoiceDto> GetInvoiceInfo(EntityDto<long> input);

    Task CreateInvoice(CreateInvoiceDto input);
}
