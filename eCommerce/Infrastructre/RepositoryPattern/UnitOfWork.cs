using Blog_Project.CORE.@interface;
using Blog_Project.EF.RepositoryPattern;
using eCommerce.Core.Data;
using eCommerce.Core.entities;
using eCommerce.Core.entities.Order;
using eCommerce.Core.Interface;
using eCommerce.Infrastructre.Data;
using Microsoft.EntityFrameworkCore.Storage;
using StackExchange.Redis;

namespace eCommerce.Infrastructre.RepositoryPattern
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly appDBcontext context;
        
        public IBaseRepository<Core.entities.Order.Order> OrderRepository { get; private set; }
        public OrderRepository customOrderRepository { get; private set; }

        public IBasketRepository basketRepository { get; private set; }

        public IPaymentService paymentRepository { get; private set; }

        public IBaseRepository<DeliveryMethod> deliveryRepository { get; private set; }

        public IBaseRepository<Product> productsRepository { get; private set; }

        public IBaseRepository<ProductBrand> productsBrandRepository { get; private set; }

        public IBaseRepository<ProductType> productsTypeRepository { get; private set; }

        public UnitOfWork(appDBcontext context, IConnectionMultiplexer redis, IConfiguration configuration)
        {
            this.context = context;
            OrderRepository = new BaseRepository<Core.entities.Order.Order>(context);
            basketRepository = new BasketRepository(redis);
            deliveryRepository = new BaseRepository<DeliveryMethod>(context);
            productsRepository = new BaseRepository<Product>(context);
            productsBrandRepository = new BaseRepository<ProductBrand>(context);
            productsTypeRepository = new BaseRepository<ProductType>(context);
            paymentRepository = new PaymentService(configuration, basketRepository, productsRepository, deliveryRepository);
            customOrderRepository = new OrderRepository(context);


        }
        public async Task<int> Complete() => await context.SaveChangesAsync();


        public void Dispose()
        {
            context.Dispose();
        }
    }
}
