using eCommerce.Core.entities;
using eCommerce.Core.Interface;
using Microsoft.AspNetCore.Components.Forms;
using StackExchange.Redis;
using System.Text.Json;

namespace eCommerce.Infrastructre.Data
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _DataBase;

        public BasketRepository(IConnectionMultiplexer redis)
        {
            _DataBase = redis.GetDatabase();
        }
        public async Task<bool> DeleteBasket(string Basketid)
        {
            return await _DataBase.KeyDeleteAsync(Basketid);
        }

        public async Task<CustomerBasket?> GetBasketById(string Basketid)
        {
            var data = await _DataBase.StringGetAsync(Basketid);
            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(data);

        }

        public async Task<CustomerBasket?> UpdateOrCreateBasket(CustomerBasket basket)
        {
            var created = await _DataBase.StringSetAsync(basket.id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(1));
            if (!created) return null;

            return await GetBasketById(basket.id);
        }
    }
}
