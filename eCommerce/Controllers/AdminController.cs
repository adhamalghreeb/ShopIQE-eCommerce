using eCommerce.Core.entities.Order;
using eCommerce.Core.Interface;
using eCommerce.DTO;
using eCommerce.Extension;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public AdminController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [HttpGet("GetAllOrders")]
        public async Task<ActionResult<IReadOnlyList<Order>>> GetAllOrders(
            [FromQuery] string? query,
            [FromQuery] string? sortBy,
            [FromQuery] string? sortDirection,
            [FromQuery] int? pageNumber,
            [FromQuery] int? pageSize)
        {
            var response = await unitOfWork.customOrderRepository.GetAllAsync(query, sortBy, sortDirection, pageNumber, pageSize);
            List<OrderDto> ordersDto = new List<OrderDto>();
            foreach (var order in response)
            {
                var orderDto = order.toDto();
                ordersDto.Add(orderDto);
            }
            return Ok(ordersDto);
        }
    }
}
