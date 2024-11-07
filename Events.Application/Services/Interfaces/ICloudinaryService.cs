using Microsoft.AspNetCore.Http;

namespace Events.Application.Services.Interfaces
{
    public interface ICloudinaryService
    {
        Task<string> UploadImage(IFormFile file);
    }
}
