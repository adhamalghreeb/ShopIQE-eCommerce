using eCommerce.Core.entities.Order;

namespace eCommerce.Core.Specifications
{
    public class OrderSpec : BaseSpecifications<Order>
    {
        public OrderSpec(string email) : base(x => x.BuyerEmail == email)
        {
            AddInclude(x => x.OrderItems);
            AddInclude(x => x.DeliveryMethod);
            AddInclude(x => x.OrderDate);
        }
        public OrderSpec(string email , int id) : base(x => x.BuyerEmail == email)
        {
            AddInclude(x => x.OrderItems);
            AddInclude(x => x.DeliveryMethod);
            AddInclude(x => x.OrderDate);
        }
    }
}
