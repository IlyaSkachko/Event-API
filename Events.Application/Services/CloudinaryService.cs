using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Events.Application.Services.Interfaces;
using Events.Domain.Cloudinary;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Events.Application.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary cloudinary;

        public CloudinaryService(CloudinarySettings cloudinarySettings)
        {
            cloudinary = new Cloudinary(cloudinarySettings.Url);
            cloudinary.Api.Secure = true;
        }

        public async Task<string> UploadImage(IFormFile file)
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream())
            };

            var uploadResult = await cloudinary.UploadAsync(uploadParams);

            return uploadResult.SecureUrl.AbsoluteUri;
        }
    }
}
