using eCommerce.Core.entities.Order;
using eCommerce.DTO;

namespace eCommerce.Extension
{
    public static class OrderMapping
    {
        public static OrderDto toDto(this Order order)
        {
            return new OrderDto()
            {
                BuyerEmail = order.BuyerEmail,
                OrderDate = order.OrderDate,
                ShipToAddress = order.getAddress(order.ShipToAddress),
                DeliveryMethod = order.DeliveryMethod.ToString(),
                Status = order.Status.ToString(),
                OrderItems = order.OrderItems.Select(x => x.toItemDto()).ToList()
            };
        }

        public static OrderItemDto toItemDto(this OrderItem orderItem)
        {
            return new OrderItemDto()
            {
                ProductName = orderItem.ItemOrdered.ProductName,
                PictureUrl = orderItem.ItemOrdered.PictureUrl,
                Price = orderItem.Price,
                Quantity = orderItem.Quantity,
            };
        }
    }
}
