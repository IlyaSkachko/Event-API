using Microsoft.AspNetCore.Http;

namespace Events.Application.Interfaces.UseCase.Cloudinary
{
    public interface IUploadImageCloudinaryUseCase
    {
        Task<string> ExecuteAsync(IFormFile file);
    }
}
