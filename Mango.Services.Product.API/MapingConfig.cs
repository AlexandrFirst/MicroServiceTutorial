using AutoMapper;
using Mango.Services.Product.API.Models.Dto;
using db = Mango.Services.Product.API.Models;

namespace Mango.Services.Product.API
{
    public class MapingConfig
    {
        public static MapperConfiguration RegisterMaps() 
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductDto, db.Product>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
