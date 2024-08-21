namespace eCommerce.Core.entities
{
    public class BasketItem
    {
        public int productId { get; set; }
        public string productName { get; set; }
        public double Price { get; set; }
        public string PictureUrl { get; set; }
        public int Quantity { get; set; }

        public string brand { get; set; }
        public string type { get; set; }
    }
}
