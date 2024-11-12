using Microsoft.AspNetCore.Http;

namespace Events.Application.UseCases.CacheUseCase.ImageCache.Interfaces
{
    public interface ICacheImageUseCase
    {
        Task<string> ExecuteAsync(int eventId, IFormFile file, Func<IFormFile, Task<string>> uploadFunction, TimeSpan cacheDuration);
    }
}
