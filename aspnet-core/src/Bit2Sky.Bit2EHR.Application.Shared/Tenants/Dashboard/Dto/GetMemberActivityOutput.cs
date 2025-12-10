using System.Collections.Generic;

namespace Bit2Sky.Bit2EHR.Tenants.Dashboard.Dto;

public class GetMemberActivityOutput
{
    public GetMemberActivityOutput(List<MemberActivity> memberActivities)
    {
        MemberActivities = memberActivities;
    }

    public List<MemberActivity> MemberActivities { get; set; }


}

