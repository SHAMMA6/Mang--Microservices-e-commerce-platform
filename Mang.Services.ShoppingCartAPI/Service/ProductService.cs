using Mang.Services.ShoppingCartAPI.Models.Dto;
using Mang.Services.ShoppingCartAPI.Service.IService;
using Newtonsoft.Json;

namespace Mang.Services.ShoppingCartAPI.Service
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            var clint = _httpClientFactory.CreateClient("Product");
            var response = await clint.GetAsync("api/products");
            var apiContent = await response.Content.ReadAsStringAsync();
            var respons = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
            if (respons.IsSuccess)
            {
                return JsonConvert.DeserializeObject<IEnumerable<ProductDto>>(Convert.ToString(respons.Resulte));
            }
            return new List<ProductDto>();
        }
    }
}
