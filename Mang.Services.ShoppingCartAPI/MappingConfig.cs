using AutoMapper;
using Mang.Services.ShoppingCartAPI.Models;
using Mang.Services.ShoppingCartAPI.Models.Dto;

namespace Mang.Services.ShoppingCartAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CartDetails,CartDetailsDto >().ReverseMap();
                config.CreateMap<CartHeader, CartHeaderDto>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
