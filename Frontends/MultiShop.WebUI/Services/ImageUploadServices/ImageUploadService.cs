using System.Net.Http.Headers;

namespace MultiShop.WebUI.Services.ImageUploadServices
{
    public class ImageUploadService : IImageUploadService
    {
        private readonly HttpClient _httpClient;

        public ImageUploadService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string?> UploadImageAsync(IFormFile file)
        {
            using var content = new MultipartFormDataContent();
            using var stream = file.OpenReadStream();
            var fileContent = new StreamContent(stream);
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
            content.Add(fileContent, "file", file.FileName);

            var response = await _httpClient.PostAsync("images", content);
            if (!response.IsSuccessStatusCode)
                return null;

            var result = await response.Content.ReadFromJsonAsync<ImageUploadResultDto>();
            return result?.Url;
        }
    }

    public class ImageUploadResultDto
    {
        public string? Url { get; set; }
    }
}
