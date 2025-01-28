using Mang.Services.OrderAPI.Models.Dto;

namespace Mang.Services.OrderAPI.Service.IService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProducts();
    }
}
