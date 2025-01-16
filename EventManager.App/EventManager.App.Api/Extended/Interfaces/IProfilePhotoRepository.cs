namespace EventManager.App.Api.Extended.Interfaces;

public interface IProfilePhotoRepository
{
    /// <summary>
    /// Method to get Image in Base64 format.
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    string GetProfilePhotoBase64Src(string fileName);

    /// <summary>
    /// Method to upload Image.
    /// </summary>
    /// <param name="file"></param>
    /// <param name="fileName"></param>
    /// <returns></returns>
    bool UploadProfilePhoto(IFormFile file, string fileName);
}
