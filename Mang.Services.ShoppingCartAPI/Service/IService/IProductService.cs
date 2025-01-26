using Mang.Services.ShoppingCartAPI.Models.Dto;

namespace Mang.Services.ShoppingCartAPI.Service.IService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProducts();
    }
}
