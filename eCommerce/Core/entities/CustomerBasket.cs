namespace eCommerce.Core.entities
{
    public class CustomerBasket
    {
        public CustomerBasket()
        {
            
        }
        public CustomerBasket(string id)
        {
            this.id = id;
        }
        public string id { get; set; }
        public List<BasketItem> items { get; set; } = new List<BasketItem>();
        public int? DeliveryMethodId { get; set; }
        public string? ClientSecret { get; set; }
        public string? PaymentIntentId { get; set; }
    }
}
