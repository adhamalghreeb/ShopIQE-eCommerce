using Blog_Project.CORE.@interface;
using eCommerce.Core.entities;
using eCommerce.Core.entities.Order;
using eCommerce.Infrastructre.Data;

namespace eCommerce.Core.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        public IBaseRepository<Order> OrderRepository { get; } 
        public IBasketRepository basketRepository { get; }
        public IPaymentService paymentRepository { get; }
        public IBaseRepository<DeliveryMethod> deliveryRepository {  get; }
        public IBaseRepository<Product> productsRepository { get; }
        public IBaseRepository<ProductBrand> productsBrandRepository { get; }
        public IBaseRepository<ProductType> productsTypeRepository { get; }
        public OrderRepository customOrderRepository { get; }

        public Task<int> Complete();
    }
}
