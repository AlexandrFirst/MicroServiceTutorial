using Mango.Services.Product.API.Models.Dto;

namespace Mango.Services.Product.API.Repositry
{
    public interface IProductRepositry
    {
        Task<IEnumerable<ProductDto>> GetProducts();
        Task<ProductDto> GetProductById(int productId);
        Task<ProductDto> CreateUpdateProduct(ProductDto productDto);
        Task<bool> DeleteProduct(int productId);
    }
}
