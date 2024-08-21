using eCommerce.Core.entities;
using eCommerce.Core.entities.Order;

namespace eCommerce.DTO
{
    public class OrderDto
    {
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public string ShipToAddress { get; set; }
        public string DeliveryMethod { get; set; }
        public string Status { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
        public PaymentSummary PaymentSummary { get; set; }

    }

    public class OrderItemDto
    {
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
