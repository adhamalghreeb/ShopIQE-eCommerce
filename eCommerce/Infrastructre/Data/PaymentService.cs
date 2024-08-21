using Blog_Project.CORE.@interface;
using eCommerce.Core.entities;
using eCommerce.Core.Interface;
using Stripe;

namespace eCommerce.Infrastructre.Data
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration configuration;
        private readonly IBasketRepository basketRepository;
        private readonly IBaseRepository<Core.entities.Product> productRepo;
        private readonly IBaseRepository<DeliveryMethod> deliveryRepo;

        public PaymentService(IConfiguration configuration, IBasketRepository basketRepository, IBaseRepository<Core.entities.Product> productRepo, IBaseRepository<DeliveryMethod> deliveryRepo)
        {
            this.configuration = configuration;
            this.basketRepository = basketRepository;
            this.productRepo = productRepo;
            this.deliveryRepo = deliveryRepo;
        }
        public async Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId)
        {
            StripeConfiguration.ApiKey = configuration["StripeSettings:Secretkey"];
            // fetch the basket
            var cart = await basketRepository.GetBasketById(basketId);

            if (cart == null) return null;

            var ShipingPrice = 0d;

            if (cart.DeliveryMethodId.HasValue)
            {
                // fetch delivery Mehtod
                var deliveryMethod = await deliveryRepo.GetById((int)cart.DeliveryMethodId);
                if (deliveryMethod == null) return null;

                ShipingPrice = deliveryMethod.Price;

            }

            foreach (var item in cart.items)
            {
                // fetch the product
                var productItem = await productRepo.GetById(item.productId);
                if (productItem == null) return null;

                // check correct price
                if (productItem.Price != item.Price)
                {
                    item.Price = productItem.Price;
                }


            }

            var service = new PaymentIntentService();
            PaymentIntent? intent = null;
            // check if there is no intent
            if (string.IsNullOrEmpty(cart.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)cart.items.Sum(x => x.Quantity * (x.Price * 100) + (long)ShipingPrice * 100),
                    Currency = "EGP",
                    PaymentMethodTypes = ["card"]
                };
                intent = await service.CreateAsync(options);
                cart.PaymentIntentId = intent.Id;
                cart.ClientSecret = intent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    // only update the amount
                    Amount = (long)cart.items.Sum(x => x.Quantity * (x.Price * 100) + (long)ShipingPrice * 100)
                };
                intent = await service.UpdateAsync(cart.PaymentIntentId, options);
            }

            await basketRepository.UpdateOrCreateBasket(cart);
            return cart;


        }


    }
}
