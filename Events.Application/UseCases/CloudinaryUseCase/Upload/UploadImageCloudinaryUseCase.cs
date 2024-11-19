using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Events.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Events.Application.Configuration.Cloudinary;
using Events.Application.Interfaces.UseCase.Cloudinary;

namespace Events.Application.UseCases.CloudinaryUseCase.Upload
{
    public class UploadImageCloudinaryUseCase : IUploadImageCloudinaryUseCase
    {
        private readonly Cloudinary cloudinary;

        public UploadImageCloudinaryUseCase(CloudinarySettings cloudinarySettings)
        {
            cloudinary = new Cloudinary(cloudinarySettings.Url);
            cloudinary.Api.Secure = true;
        }

        public async Task<string> ExecuteAsync(IFormFile file)
        {
            if (file is null)
            {
                throw new BadRequestException("File data is missing");
            }

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream())
            };

            var uploadResult = await cloudinary.UploadAsync(uploadParams);

            return uploadResult.SecureUrl.AbsoluteUri;
        }
    }
}
