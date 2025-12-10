using System.ComponentModel.DataAnnotations;
using Abp.Runtime.Validation;
using Bit2Sky.Bit2EHR.Common;
using Bit2Sky.Bit2EHR.Dto;

namespace Bit2Sky.Bit2EHR.Organizations.Dto;

public class GetOrganizationUnitRolesInput : PagedAndSortedInputDto, IShouldNormalize
{
    [Range(1, long.MaxValue)]
    public long Id { get; set; }

    public void Normalize()
    {
        if (string.IsNullOrEmpty(Sorting))
        {
            Sorting = "role.DisplayName, role.Name";
        }

        Sorting = DtoSortingHelper.ReplaceSorting(Sorting, s =>
        {
            if (s.Contains("displayName"))
            {
                s = s.Replace("displayName", "role.displayName");
            }

            if (s.Contains("addedTime"))
            {
                s = s.Replace("addedTime", "ouRole.creationTime");
            }

            return s;
        });
    }
}

