using Abp.Runtime.Validation;
using Bit2Sky.Bit2EHR.Common;
using Bit2Sky.Bit2EHR.Dto;

namespace Bit2Sky.Bit2EHR.MultiTenancy.Payments.Dto;

public class GetPaymentHistoryInput : PagedAndSortedInputDto, IShouldNormalize
{
    public void Normalize()
    {
        if (string.IsNullOrEmpty(Sorting))
        {
            Sorting = "CreationTime";
        }

        Sorting = DtoSortingHelper.ReplaceSorting(Sorting, s =>
        {
            return s.Replace("editionDisplayName", "Edition.DisplayName");
        });
    }
}

