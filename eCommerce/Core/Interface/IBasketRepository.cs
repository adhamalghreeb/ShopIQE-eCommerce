using eCommerce.Core.entities;

namespace eCommerce.Core.Interface
{
    public interface IBasketRepository
    {
        Task<CustomerBasket?> GetBasketById(string Basketid);
        Task<CustomerBasket?> UpdateOrCreateBasket(CustomerBasket basket);
        Task<bool> DeleteBasket(string Basketid);
    }
}
