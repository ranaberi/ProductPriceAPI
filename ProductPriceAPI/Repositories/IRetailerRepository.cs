namespace ProductPriceAPI.Repositories
{
    public interface IRetailerRepository
    {
        Task<decimal> GetHighestTier1PriceAsync(string ean);
        Task UpdatePriceForProductAsync(int retailerId, string ean, decimal newPrice);
    }
}
