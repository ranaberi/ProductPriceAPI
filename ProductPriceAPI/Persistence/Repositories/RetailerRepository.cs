using Microsoft.EntityFrameworkCore;
using ProductPriceAPI.Models;
using ProductPriceAPI.Persistence.Contexts;
using ProductPriceAPI.Persistence.Repositories;
using ProductPriceAPI.Repositories;

public class RetailerRepository : BaseRepository, IRetailerRepository
{
    private readonly ILogger<RetailerRepository> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public RetailerRepository(AppDbContext context, ILogger<RetailerRepository> logger, IUnitOfWork unitOfWork) : base(context)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<decimal> GetHighestTier1PriceAsync(string ean)
    {
        try
        {
            return await _context.ProductPrices
                .Include(pp => pp.Product)
                .Where(pp => pp.Product.EAN == ean)
                .OrderByDescending(pp => pp.Price)
                .Select(pp => pp.Price)
                .FirstOrDefaultAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting highest tier 1 price for product {ean}");
            throw;
        }
    }

    public async Task UpdatePriceForProductAsync(int retailerId, string ean, decimal newPrice)
    {
        try
        {
            var productPrice = await _context.ProductPrices
                .Include(pp => pp.Product)
                .Where(pp => pp.RetailerId == retailerId && pp.Product.EAN == ean)
                .SingleOrDefaultAsync();

            if (productPrice == null)
            {
                var product = await _context.Products.SingleOrDefaultAsync(p => p.EAN == ean);
                if (product == null)
                {
                    throw new Exception("Product not found");
                }

                productPrice = new ProductPrice { RetailerId = retailerId, ProductId = product.Id, Price = newPrice };
                await _context.ProductPrices.AddAsync(productPrice);
            }
            else
            {
                productPrice.Price = newPrice;
            }

            await _unitOfWork.CompleteAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error updating price for product {ean} for retailer {retailerId}");
            throw;
        }
    }
}