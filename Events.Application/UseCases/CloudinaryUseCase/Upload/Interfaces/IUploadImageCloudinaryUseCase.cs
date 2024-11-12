using Microsoft.AspNetCore.Http;

namespace Events.Application.UseCases.CloudinaryUseCase.Upload.Interfaces
{
    public interface IUploadImageCloudinaryUseCase
    {
        Task<string> ExecuteAsync(IFormFile file);
    }
}
