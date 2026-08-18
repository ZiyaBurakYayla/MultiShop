using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Options;
using MultiShop.Images.Settings;

namespace MultiShop.Images.Services.StorageServices
{
    public class GoogleCloudStorageService : IStorageService
    {
        private readonly GoogleCloudStorageSettings _settings;
        private readonly StorageClient _storageClient;

        public GoogleCloudStorageService(IOptions<GoogleCloudStorageSettings> options)
        {
            _settings = options.Value;

            // Service account json anahtari ile kimlik dogrulama
            var credential = GoogleCredential.FromFile(_settings.CredentialFilePath);
            _storageClient = StorageClient.Create(credential);
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            // Her dosyaya benzersiz ad ver (uzantiyi koru)
            var objectName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

            using var stream = file.OpenReadStream();
            var uploadedObject = await _storageClient.UploadObjectAsync(
                bucket: _settings.BucketName,
                objectName: objectName,
                contentType: file.ContentType,
                source: stream);

            // Public erisilebilir URL (bucket public ise dogrudan acilir)
            return $"https://storage.googleapis.com/{_settings.BucketName}/{objectName}";
        }
    }
}
