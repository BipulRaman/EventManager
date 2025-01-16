using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using EventManager.App.Api.Basic.Models;
using EventManager.App.Api.Extended.Interfaces;
using Microsoft.Extensions.Options;

namespace EventManager.App.Api.Extended.Services;

public class ProfilePhotoRepository : IProfilePhotoRepository
{
    private readonly BlobContainerClient blobContainerClient;

    public ProfilePhotoRepository(IOptions<AzureTableConfig> azureTableConfig)
    {
        blobContainerClient = new BlobContainerClient(azureTableConfig.Value.ConnectionString, azureTableConfig.Value.ProfilePhotosContainer);
    }

    public string GetProfilePhotoBase64Src(string fileName)
    {
        try
        {
            BlobClient blobClient = blobContainerClient.GetBlobClient(fileName);
            BlobDownloadResult response = blobClient.DownloadContent();
            string imageBase64 = Convert.ToBase64String(response.Content.ToArray());
            string imageSrc = string.Format("data:image/jpeg;base64,{0}", imageBase64);
            return imageSrc;
        }
        catch (Exception)
        {
            return string.Empty;
        }
    }

    public bool UploadProfilePhoto(IFormFile file, string fileName)
    {
        try
        {
            BlobClient blobClient = blobContainerClient.GetBlobClient(fileName);
            blobClient.DeleteIfExists();
            blobClient.Upload(file.OpenReadStream());
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
