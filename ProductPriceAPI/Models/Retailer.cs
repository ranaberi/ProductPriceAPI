namespace ProductPriceAPI.Models
{
    public class Retailer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ProductPrice> ProductPrices { get; set; }
    }
}
