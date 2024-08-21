using Blog_Project.CORE.@interface;
using eCommerce.Core.entities;
using eCommerce.Core.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        
        public IUnitOfWork unitOfWork { get; }

        public PaymentController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpPost("{cardID}")]
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent(string cardID)
        {
            var cart  = await unitOfWork.paymentRepository.CreateOrUpdatePaymentIntent(cardID);
            if (cart == null) return BadRequest("Problem With your cart");
            return Ok(cart);
        }

        [HttpGet("delivery-methods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {

            return Ok(await unitOfWork.deliveryRepository.ListAllAsync());
        }
    }
}
