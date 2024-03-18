namespace ProductPriceAPI.Services
{
    public interface IRetailerService
    {
        Task<decimal> GetHighestTier1PriceAsync(string ean);
        Task UpdatePriceForProductAsync(int retailerId, string ean, decimal newPrice);
    }
}
