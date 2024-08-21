using eCommerce.Core.Interface;
using StackExchange.Redis;
using System.Text.Json;

namespace eCommerce.Infrastructre
{
    public class ResponseCache : IResponseCache
    {
        private readonly IDatabase _database;
        public ResponseCache(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase(1);
        }
        public async Task CacheResponseAsync(string cacheKey, object response, TimeSpan TTL)
        {
            var serializeOptions = JsonSerializer.Serialize(response);
            await _database.StringSetAsync(cacheKey, serializeOptions, TTL);
        }

        public async Task<string?> GetCacheResponse(string cacheKey)
        {
            var cachedResponse = await _database.StringGetAsync(cacheKey);
            if (cachedResponse.IsNullOrEmpty) return null;

            return cachedResponse;
        }

        public Task<string?> RemoveCacheResponse(string pattern)
        {
            throw new NotImplementedException();
        }
    }
}
