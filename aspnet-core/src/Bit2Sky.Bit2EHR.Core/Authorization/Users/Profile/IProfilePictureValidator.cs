using System.Threading.Tasks;

namespace Bit2Sky.Bit2EHR.Authorization.Users.Profile;

public interface IProfilePictureValidator
{
    Task ValidateProfilePictureDimensions(byte[] imageBytes);
    Task ValidateProfilePictureSize(byte[] imageBytes);
}