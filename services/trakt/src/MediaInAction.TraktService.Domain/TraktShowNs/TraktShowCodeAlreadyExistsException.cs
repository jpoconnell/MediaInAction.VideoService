using Volo.Abp;

namespace MediaInAction.TraktService.TraktShowNs
{
    public class ProductCodeAlreadyExistsException : BusinessException
    {
        public ProductCodeAlreadyExistsException(string productCode)
            : base("TraktService:000001", $"A product with code {productCode} has already exists!")
        {
            WithData("productCode", productCode);
        }
    }
}