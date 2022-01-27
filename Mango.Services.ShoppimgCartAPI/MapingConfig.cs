using AutoMapper;
using Mango.Services.ShoppimgCartAPI.Models;
using Mango.Services.ShoppimgCartAPI.Models.Dto;

namespace Mango.Services.ShoppimgCartAPI
{
    public class MapingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductDto, Product>().ReverseMap();
                config.CreateMap<CartHeader, CartHeaderDto>().ReverseMap();
                config.CreateMap<CartDetails, CartDetailsDto>().ReverseMap();
                config.CreateMap<Cart, CartDto>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
