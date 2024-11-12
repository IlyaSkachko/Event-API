using Events.Application.UseCases.CacheUseCase.ImageCache.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;

namespace Events.Application.UseCases.CacheUseCase.ImageCache
{
    public class CacheImageUseCase : ICacheImageUseCase
    {
        private readonly IMemoryCache memoryCache;
        
        public CacheImageUseCase(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        public async Task<string> ExecuteAsync(int eventId, IFormFile file, Func<IFormFile, Task<string>> uploadFunction, TimeSpan cacheDuration)
        {
            string cacheKey = $"event_{eventId}_image"; 

            if (!memoryCache.TryGetValue(cacheKey, out string cachedValue))
            {
                cachedValue = await uploadFunction(file); 
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(cacheDuration); 
                memoryCache.Set(cacheKey, cachedValue, cacheEntryOptions);
            }
            return cachedValue;
        }
    }
}
