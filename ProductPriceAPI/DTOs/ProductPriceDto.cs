namespace ProductPriceAPI.DTOs
{
    public class ProductPriceDto
    {
        public decimal Price { get; set; }
        public string RetailerName { get; set; }

        public int RetailerId { get; set; }
    }
}
