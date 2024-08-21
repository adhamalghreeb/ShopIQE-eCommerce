using eCommerce.Core.entities;
using eCommerce.Core.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        
        public IUnitOfWork UnitOfWork { get; }

        public BasketController(IUnitOfWork UnitOfWork)
        {
            this.UnitOfWork = UnitOfWork;  
        }
        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasketById(string id)
        {
            var basket = await UnitOfWork.basketRepository.GetBasketById(id);
            return Ok(basket ?? new CustomerBasket());
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket customerBasket)
        {
            var updatedBasket = await UnitOfWork.basketRepository.UpdateOrCreateBasket(customerBasket);
            
            return Ok(updatedBasket);
        }

        [HttpDelete]
        public async Task DeleteBasket(string id)
        {
            await UnitOfWork.basketRepository.DeleteBasket(id);
            
        }
    }
}
