using Blog_Project.EF.RepositoryPattern;
using eCommerce.Core.Data;
using eCommerce.Core.entities.Order;
using eCommerce.Core.Interface;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Infrastructre.Data
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        private readonly appDBcontext appDBcontext;

        public OrderRepository(appDBcontext appDBcontext) : base(appDBcontext) => this.appDBcontext = appDBcontext;
        public async Task<IEnumerable<Order>> GetAllAsync(string? query = null, string? sortBy = null, string? sortDirection = null, int? pageNumber = 1, int? pageSize = 5)
        {
            // query
            var orders = appDBcontext.orders
                .Include(o => o.DeliveryMethod)
                .Include(o => o.OrderItems)
                .Include(o => o.PaymentSummary)
                .AsQueryable();

            // filter
            if (string.IsNullOrWhiteSpace(query) == false)
            {
                orders = orders.Where(x => x.BuyerEmail.Contains(query));
            }

            // sort
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                // sort by name
                if (string.Equals(sortBy, "Name", StringComparison.OrdinalIgnoreCase))
                {
                    var isAsc = string.Equals(sortDirection, "asc", StringComparison.OrdinalIgnoreCase);
                    orders = isAsc ? orders.OrderBy(x => x.BuyerEmail) : orders.OrderByDescending(x => x.BuyerEmail);
                }

                // sort by url
                if (string.Equals(sortBy, "URL", StringComparison.OrdinalIgnoreCase))
                {
                    var isAsc = string.Equals(sortDirection, "asc", StringComparison.OrdinalIgnoreCase);
                    orders = isAsc ? orders.OrderBy(x => x.OrderDate) : orders.OrderByDescending(x => x.OrderDate);
                }
            }



            // paginagtion
            var skipResult = (pageNumber - 1) * pageSize;
            orders = orders.Skip(skipResult ?? 0).Take(pageSize ?? 5);

            return await orders.ToListAsync();
        }
    }
}
