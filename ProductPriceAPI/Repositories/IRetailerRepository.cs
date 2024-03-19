using ProductPriceAPI.Repositories.Communication;

namespace ProductPriceAPI.Repositories
{
    public interface IRetailerRepository
    {
        Task<decimal> GetHighestTier1PriceAsync(string ean);
        Task<SavePriceResponse> UpdatePriceForProductAsync(int retailerId, string ean, decimal newPrice);

    }
}
