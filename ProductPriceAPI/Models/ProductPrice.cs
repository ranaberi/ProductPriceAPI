namespace ProductPriceAPI.Models
{
    public class ProductPrice
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public DateTime Date { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int RetailerId { get; set; }
        public Retailer Retailer { get; set; }
    }
}
