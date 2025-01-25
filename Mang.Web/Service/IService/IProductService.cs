
using Mang.Web.Models;

namespace Mang.Web.Service.IService
{
    public interface IProductService
    {
        Task<ResponseDto?> GetProductAsync(string name);
        Task<ResponseDto?> GetProductByIdAsync(int id);
        Task<ResponseDto?> GetAllProductsAsync();
        Task<ResponseDto?> CreateProductsAsync(ProductDto productDto);
        Task<ResponseDto?> UpdateProductsAsync(ProductDto productDto);
        Task<ResponseDto?> DeleteProductsAsync(int id);
    }
}
