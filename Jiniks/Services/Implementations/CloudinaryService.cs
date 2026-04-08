using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Jiniks.Infrastructure;
using Jiniks.Models.Enums;
using Jiniks.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace Jiniks.Services.Implementations;

public class CloudinaryService : ICloudinaryService
{
    private readonly Cloudinary _cloudinary;
    private readonly ILogger<CloudinaryService> _logger;

    public CloudinaryService(IOptions<CloudinarySettings> settings, ILogger<CloudinaryService> logger)
    {
        _logger = logger;
        var account = new Account(settings.Value.CloudName, settings.Value.ApiKey, settings.Value.ApiSecret);
        _cloudinary = new Cloudinary(account);
    }

    public async Task<CloudinaryUploadResult> UploadImageAsync(IFormFile file, string folder)
    {
        _logger.LogInformation("Uploading image {FileName} to folder {Folder}", file.FileName, folder);

        await using var stream = file.OpenReadStream();
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(file.FileName, stream),
            Folder = $"jiniks/{folder}"
        };

        var result = await _cloudinary.UploadAsync(uploadParams);

        if (result.Error != null)
        {
            _logger.LogError("Cloudinary image upload failed: {Error}", result.Error.Message);
            throw new InvalidOperationException($"Image upload failed: {result.Error.Message}");
        }

        _logger.LogInformation("Image uploaded successfully with PublicId {PublicId}", result.PublicId);

        return new CloudinaryUploadResult
        {
            Url = result.SecureUrl.ToString(),
            PublicId = result.PublicId
        };
    }

    public async Task<CloudinaryUploadResult> UploadVideoAsync(IFormFile file, string folder)
    {
        _logger.LogInformation("Uploading video {FileName} to folder {Folder}", file.FileName, folder);

        await using var stream = file.OpenReadStream();
        var uploadParams = new VideoUploadParams
        {
            File = new FileDescription(file.FileName, stream),
            Folder = $"jiniks/{folder}"
        };

        var result = await _cloudinary.UploadAsync(uploadParams);

        if (result.Error != null)
        {
            _logger.LogError("Cloudinary video upload failed: {Error}", result.Error.Message);
            throw new InvalidOperationException($"Video upload failed: {result.Error.Message}");
        }

        _logger.LogInformation("Video uploaded successfully with PublicId {PublicId}", result.PublicId);

        return new CloudinaryUploadResult
        {
            Url = result.SecureUrl.ToString(),
            PublicId = result.PublicId
        };
    }

    public async Task<CloudinaryUploadResult> UploadRawFileAsync(IFormFile file, string folder)
    {
        _logger.LogInformation("Uploading raw file {FileName} to folder {Folder}", file.FileName, folder);

        await using var stream = file.OpenReadStream();
        var uploadParams = new RawUploadParams
        {
            File = new FileDescription(file.FileName, stream),
            Folder = $"jiniks/{folder}"
        };

        var result = await _cloudinary.UploadAsync(uploadParams);

        if (result.Error != null)
        {
            _logger.LogError("Cloudinary raw file upload failed: {Error}", result.Error.Message);
            throw new InvalidOperationException($"File upload failed: {result.Error.Message}");
        }

        _logger.LogInformation("Raw file uploaded successfully with PublicId {PublicId}", result.PublicId);

        return new CloudinaryUploadResult
        {
            Url = result.SecureUrl.ToString(),
            PublicId = result.PublicId
        };
    }

    public async Task<bool> DeleteAsync(string publicId, MediaType mediaType)
    {
        _logger.LogInformation("Deleting asset {PublicId} of type {MediaType}", publicId, mediaType);

        var resourceType = mediaType switch
        {
            MediaType.Video => ResourceType.Video,
            _ => ResourceType.Image
        };

        var result = await _cloudinary.DestroyAsync(new DeletionParams(publicId) { ResourceType = resourceType });

        if (result.Result == "ok")
        {
            _logger.LogInformation("Asset {PublicId} deleted successfully", publicId);
            return true;
        }

        _logger.LogWarning("Failed to delete asset {PublicId}: {Result}", publicId, result.Result);
        return false;
    }
}
