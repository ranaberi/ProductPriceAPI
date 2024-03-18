namespace ProductPriceAPI.DTOs
{
    public class ProductDto
    {
        public string EAN { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IssuingCountry { get; set; }
        public List<ProductPriceDto> ProductPrices { get; set; }
    }
}
