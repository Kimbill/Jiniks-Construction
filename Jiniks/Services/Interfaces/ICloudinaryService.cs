using Jiniks.Models.Enums;

namespace Jiniks.Services.Interfaces;

public class CloudinaryUploadResult
{
    public string Url { get; set; } = string.Empty;
    public string PublicId { get; set; } = string.Empty;
}

public interface ICloudinaryService
{
    Task<CloudinaryUploadResult> UploadImageAsync(IFormFile file, string folder);
    Task<CloudinaryUploadResult> UploadVideoAsync(IFormFile file, string folder);
    Task<CloudinaryUploadResult> UploadRawFileAsync(IFormFile file, string folder);
    Task<bool> DeleteAsync(string publicId, MediaType mediaType);
}
