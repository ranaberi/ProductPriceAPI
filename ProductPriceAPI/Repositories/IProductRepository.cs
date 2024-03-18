using ProductPriceAPI.DTOs;

namespace ProductPriceAPI.Repositories
{
    public interface IProductRepository
    {
        Task<ProductDto> GetProductByEANAsync(string ean);
        Task<IEnumerable<ProductPriceDto>> ListPricesForProductAsync(string ean, int pageNumber = 1, int pageSize = 10, decimal? minPrice = null, decimal? maxPrice = null);

        Task<IEnumerable<RetailerDto>> ListCompetitorsForProductAsync(string ean);
        Task<decimal> GetPriceRecommendationForProductAsync(string ean);
    }
}
