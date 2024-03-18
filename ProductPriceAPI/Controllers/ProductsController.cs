using Microsoft.AspNetCore.Mvc;
using ProductPriceAPI.Services;

namespace ProductPriceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController: ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("{ean}")]
        public async Task<IActionResult> GetProductAsync(string ean)
        {
            var products = await _productService.GetProductByEANAsync(ean);
            if (products == null)
            {
                return NotFound();
            }
            return Ok(products);
        }

        [HttpGet("{ean}/prices")]
        public async Task<IActionResult> GetAllPricesForProductAsync(string ean)
        {
            var prices = await _productService.ListPricesForProductAsync(ean);
            if (prices == null)
            {
                return NotFound();
            }
            return Ok(prices);
        }

        [HttpGet("{ean}/competitors")]
        public async Task<IActionResult> GetAllCompetitorsForProductAsync(string ean)
        {
            var competitors = await _productService.ListCompetitorsForProductAsync(ean);
            if (competitors == null)
            {
                return NotFound();
            }
            return Ok(competitors);
        }

        [HttpGet("{ean}/priceRecommendation")]
        public async Task<IActionResult> GetPriceRecommendationForProductAsync(string ean)
        {
            var priceRecommendation = await _productService.GetPriceRecommendationForProductAsync(ean);
            return Ok(priceRecommendation);
        }

    }
}
