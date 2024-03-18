using ProductPriceAPI.Repositories;

namespace ProductPriceAPI.Services
{
    public class RetailerService: IRetailerService
    {
        private readonly IRetailerRepository _retailerRepository;

        public RetailerService(IRetailerRepository retailerRepository)
        {
            _retailerRepository = retailerRepository;
        }

        public async Task<decimal> GetHighestTier1PriceAsync(string ean)
        {
            return await _retailerRepository.GetHighestTier1PriceAsync(ean);
        }

        public async Task UpdatePriceForProductAsync(int retailerId, string ean, decimal newPrice)
        {
            await _retailerRepository.UpdatePriceForProductAsync(retailerId, ean, newPrice);
        }
    }
}
