using Microsoft.AspNetCore.Mvc;
using ProductPriceAPI.Services;

namespace ProductPriceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RetailersController: ControllerBase
    {
        private readonly IRetailerService _retailerService;

        public RetailersController(IRetailerService retailerService)
        {
            _retailerService = retailerService;
        }

        [HttpGet("{ean}/highestTier1Price")]
        public async Task<IActionResult> GetHighestTier1PriceAsync(string ean)
        {
            var price = await _retailerService.GetHighestTier1PriceAsync(ean);
            return Ok(price);
        }

        [HttpPut("{retailerId}/products/{ean}/price")]
        public async Task<IActionResult> UpdatePriceForProductAsync(int retailerId, string ean,[FromBody] decimal newPrice)
        {
            try
            {
                await _retailerService.UpdatePriceForProductAsync(retailerId, ean, newPrice);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
