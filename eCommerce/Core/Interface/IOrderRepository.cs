using Blog_Project.CORE.@interface;
using eCommerce.Core.entities.Order;

namespace eCommerce.Core.Interface
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        Task<IEnumerable<Order>> GetAllAsync(
            string? query = null,
            string? sortBy = null,
            string? sortDirection = null,
            int? pageNumber = 1,
            int? pageSize = 5
            );
    }
}
