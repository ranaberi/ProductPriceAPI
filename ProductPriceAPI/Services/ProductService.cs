using ProductPriceAPI.DTOs;
using ProductPriceAPI.Repositories;

namespace ProductPriceAPI.Services
{
    public class ProductService: IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductDto> GetProductByEANAsync(string ean)
        {
            return await _productRepository.GetProductByEANAsync(ean);
        }

        public async Task<IEnumerable<ProductPriceDto>> ListPricesForProductAsync(string ean, int pageNumber = 1, int pageSize = 10, decimal? minPrice = null, decimal? maxPrice = null)
        {
            return await _productRepository.ListPricesForProductAsync(ean);
        }

        public async Task<IEnumerable<RetailerDto>> ListCompetitorsForProductAsync(string ean)
        {
            return await _productRepository.ListCompetitorsForProductAsync(ean);
        }

        public async Task<decimal> GetPriceRecommendationForProductAsync(string ean)
        {
            return await _productRepository.GetPriceRecommendationForProductAsync(ean);   
        }

    }
}
