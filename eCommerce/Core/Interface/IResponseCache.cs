namespace eCommerce.Core.Interface
{
    public interface IResponseCache
    {
        Task CacheResponseAsync(string cacheKey, object response, TimeSpan TTL);
        Task<string?> GetCacheResponse(string cacheKey);
        Task<string?> RemoveCacheResponse(string pattern);
    }
}
