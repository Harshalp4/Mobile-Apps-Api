namespace Bit2Sky.Bit2EHR.Maui.Services.User;

public interface IUserProfileService
{
    Task<string> GetProfilePicture(long userId);

    string GetDefaultProfilePicture();
}