namespace eCommerce.Core.entities.Order
{
    public class Order : BaseEntity
    {
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public OrderAddress ShipToAddress { get; set; } = null!;
        public DeliveryMethod DeliveryMethod { get; set; } = null!;
        public List<OrderItem> OrderItems { get; set; } = [];
        public decimal Subtotal { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public string PaymentIntentId { get; set; }
        public PaymentSummary PaymentSummary { get; set; } = null!;

        public string getAddress(OrderAddress ShipToAddress)
        {
            return ShipToAddress.Street + " " + ShipToAddress.City + " " + ShipToAddress.State;
        }
    }
}
