namespace MultiShop.WebUI.Services.ImageUploadServices
{
    public interface IImageUploadService
    {
        Task<string?> UploadImageAsync(IFormFile file);
    }
}
