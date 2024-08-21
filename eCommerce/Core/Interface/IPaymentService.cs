using eCommerce.Core.entities;

namespace eCommerce.Core.Interface
{
    public interface IPaymentService
    {
        Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId);
    }
}
