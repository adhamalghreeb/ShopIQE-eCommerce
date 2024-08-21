using eCommerce.Core.entities.Order;
using System.ComponentModel.DataAnnotations;

namespace eCommerce.DTO
{
    public class CreateOrderDto
    {
        [Required]
        public string BasketId { get; set; } = string.Empty;
        [Required]
        public int DeliveryMethodId { get; set; }
        [Required]
        public OrderAddress orderAddress { get; set; }
        [Required]
        public PaymentSummary paymentSummary { get; set; } = null!;

    }
}
