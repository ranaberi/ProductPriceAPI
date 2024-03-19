using Microsoft.EntityFrameworkCore;
using ProductPriceAPI.Models;
using ProductPriceAPI.Persistence.Contexts;
using ProductPriceAPI.Persistence.Repositories;
using ProductPriceAPI.Repositories;
using ProductPriceAPI.Repositories.Communication;

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

    public async Task<SavePriceResponse> UpdatePriceForProductAsync(int retailerId, string ean, decimal newPrice)
    {
        var productPrice = await _context.ProductPrices
            .FirstOrDefaultAsync(pp => pp.RetailerId == retailerId && pp.Product.EAN == ean);

        if (productPrice == null)
        {
            return new SavePriceResponse("Product price not found.");
        }

        productPrice.Price = newPrice;

        try
        {
            _context.ProductPrices.Update(productPrice);
            await _unitOfWork.CompleteAsync();

            return new SavePriceResponse(productPrice);
        }
        catch (Exception ex)
        {
            return new SavePriceResponse($"An error occurred when updating the product price: {ex.Message}");
        }
    }
}