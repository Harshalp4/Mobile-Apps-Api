namespace Bit2Sky.Bit2EHR.Chat;

public interface IChatFeatureChecker
{
    void CheckChatFeatures(int? sourceTenantId, int? targetTenantId);
}

