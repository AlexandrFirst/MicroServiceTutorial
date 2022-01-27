using AutoMapper;

namespace Mango.Services.OrderAPI
{
    public  static class MapingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
              
            });
            return mappingConfig;
        }
    }
}
