namespace MultiShop.Images.Services.StorageServices
{
    public interface IStorageService
    {
        // Dosyayi Google Cloud Storage'a yukler ve erisilebilir URL'i doner
        Task<string> UploadFileAsync(IFormFile file);
    }
}
