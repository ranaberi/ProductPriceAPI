using Microsoft.EntityFrameworkCore;
using ProductPriceAPI.Persistence.Contexts;
using ProductPriceAPI.Repositories;
using ProductPriceAPI.DTOs;

namespace ProductPriceAPI.Persistence.Repositories
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        private readonly ILogger<ProductRepository> _logger;

        public ProductRepository(AppDbContext context, ILogger<ProductRepository> logger) : base(context)
        {
            _logger = logger;
        }

        public async Task<ProductDto> GetProductByEANAsync(string ean)
        {
            try
            {
                return await _context.Products
                .Where(p => p.EAN == ean)
                    .Select(p => new ProductDto
                    {
                        EAN = p.EAN,
                        Name = p.Name,
                        Description = p.Description,
                        IssuingCountry = p.IssuingCountry,
                        ProductPrices = p.ProductPrices.Select(pp => new ProductPriceDto
                        {
                            Price = pp.Price,
                            RetailerName = pp.Retailer.Name
                        }).ToList()
                    })
                    .SingleOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting product by EAN {ean}");
                throw;
            }
        }

        public async Task<decimal> GetPriceRecommendationForProductAsync(string ean)
        {
            try
            {
                var prices = await _context.ProductPrices
                    .Include(pp => pp.Product)
                    .Where(pp => pp.Product.EAN == ean)
                    .Select(pp => pp.Price)
                    .ToListAsync();

                return prices.Any() ? prices.Average() * 0.9m : 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting price recommendation for product {ean}");
                throw;
            }
        }

        public async Task<IEnumerable<RetailerDto>> ListCompetitorsForProductAsync(string ean)
        {
            try
            {
                return await _context.Retailers
                    .Include(r => r.ProductPrices)
                        .ThenInclude(pp => pp.Product)
                    .Where(r => r.ProductPrices.Any(pp => pp.Product.EAN == ean))
                    .Select(r => new RetailerDto
                    {
                        Name = r.Name,
                        ProductPrices = r.ProductPrices
                            .Where(pp => pp.Product.EAN == ean)
                            .Select(pp => pp.Price)
                            .ToList()
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error listing competitors for product {ean}");
                throw;
            }
        }

        public async Task<IEnumerable<ProductPriceDto>> ListPricesForProductAsync(string ean, int pageNumber = 1, int pageSize = 10, decimal? minPrice = null, decimal? maxPrice = null)
        {
            try
            {
                var query = _context.ProductPrices
                    .Include(pp => pp.Product)
                    .Include(pp => pp.Retailer)
                    .Where(pp => pp.Product.EAN == ean);

                if (minPrice.HasValue)
                {
                    query = query.Where(pp => pp.Price >= minPrice.Value);
                }

                if (maxPrice.HasValue)
                {
                    query = query.Where(pp => pp.Price <= maxPrice.Value);
                }

                return await query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .Select(pp => new ProductPriceDto
                    {
                        Price = pp.Price,
                        RetailerName = pp.Retailer.Name,
                        RetailerId = pp.RetailerId
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error listing prices for product {ean}");
                throw;
            }
        }
    }
}