namespace ProductPriceAPI.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string EAN { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public string IssuingCountry { get; set; }
        public ICollection<ProductPrice> ProductPrices { get; set; }

    }
}
