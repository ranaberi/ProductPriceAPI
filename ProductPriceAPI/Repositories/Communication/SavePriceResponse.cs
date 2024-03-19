using ProductPriceAPI.Models;

namespace ProductPriceAPI.Repositories.Communication
{
    public class SavePriceResponse : BaseResponse
    {
        public ProductPrice ProductPrice { get; private set; }

        private SavePriceResponse(bool success, string message, ProductPrice productPrice) : base(success, message)
        {
            ProductPrice = productPrice;
        }

        public SavePriceResponse(ProductPrice productPrice) : this(true, string.Empty, productPrice)
        { }

        public SavePriceResponse(string message) : this(false, message, null)
        { }
    }
}
