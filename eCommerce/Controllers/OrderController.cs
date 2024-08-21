using eCommerce.Core.entities;
using eCommerce.Core.entities.Order;
using eCommerce.Core.Interface;
using eCommerce.Core.Specifications;
using eCommerce.DTO;
using eCommerce.Extension;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace eCommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public OrderController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        [HttpPost]
        public async Task<ActionResult<Order>> createOrder(CreateOrderDto createOrder)
        {
            // get user email from claims
            var email = User.GetEmail();

            // get cart
            var cart = await unitOfWork.basketRepository.GetBasketById(createOrder.BasketId);
            
            if(cart == null)
            {
                return BadRequest("no cart has been found");
            }

            if(cart.PaymentIntentId == null)
            {
                return BadRequest("no payment has been found");
            }

            // add all items from cart in new orderitem(productItem)
            var orderList = new List<OrderItem>();
            foreach (var item in cart.items)
            {
                var product = await unitOfWork.productsRepository.GetById(item.productId);
                if(product == null)
                {
                    return BadRequest("Problem with the item in the order");
                }
                var productItem = new ProductItemOrdered
                {
                    ProductItemId = product.Id,
                    ProductName = product.Name,
                    PictureUrl = product.PictureUrl
                };

                var orderItem = new OrderItem
                {
                    ItemOrdered = productItem,
                    Price = (decimal)item.Price,
                    Quantity = item.Quantity,
                };

                orderList.Add(orderItem);
            }

            // fetch deliveryMethod
            if (cart.DeliveryMethodId is null)
            {
                return BadRequest("Problem With DeliveryMethod");
            }
            
            var deliveryMethod = await unitOfWork.deliveryRepository.GetById((int)cart.DeliveryMethodId);

            // Make Order
            var order = new Order
            {
                BuyerEmail = email,
                ShipToAddress = createOrder.orderAddress,
                DeliveryMethod = deliveryMethod,
                OrderItems = orderList,
                Subtotal = orderList.Sum(x => x.Price * x.Quantity),
                PaymentIntentId = cart.PaymentIntentId,
                PaymentSummary = createOrder.paymentSummary,

            };

            await unitOfWork.OrderRepository.Add(order);

            if(await unitOfWork.Complete() > 0)
            {
                return order;
            }

            return BadRequest("error while creating the order");
            

        }

        [HttpGet]
        public async Task<ActionResult<OrderDto>> getUserOrder()
        {
            var email = User.GetEmail();

            // var spec = new OrderSpec(email);

            var orders = await unitOfWork.OrderRepository.FindAll(x => x.BuyerEmail == email , new[] { "OrderItems", "DeliveryMethod" });

            var ordersDto = orders.Select(o => o.toDto()).ToList();

            return Ok(ordersDto);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<OrderDto>> getOrderbyId(int id)
        {
            var email = User.GetEmail();

            var spec = new OrderSpec(email , id);

            var order = await unitOfWork.OrderRepository.GetEntityWithSpec(spec);
            var orderDto = order.toDto();

            return Ok(orderDto);
        }

    }
}
