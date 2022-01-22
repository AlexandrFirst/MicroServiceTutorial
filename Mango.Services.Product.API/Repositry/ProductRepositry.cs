using AutoMapper;
using Mango.Services.Product.API.DbContexts;
using Mango.Services.Product.API.Models.Dto;
using Microsoft.EntityFrameworkCore;
using models = Mango.Services.Product.API.Models;

namespace Mango.Services.Product.API.Repositry
{
    public class ProductRepositry : IProductRepositry
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;

        public ProductRepositry(ApplicationDbContext _db, IMapper _mapper)
        {
            db = _db;
            mapper = _mapper;
        }

        public async Task<ProductDto> CreateUpdateProduct(ProductDto productDto)
        {
            models.Product product = mapper.Map<ProductDto, models.Product>(productDto);
            if (product.ProductId > 0)
            {
                db.Update(product);
            }
            else
            {
                await db.AddAsync(product);
            }
            
            await db.SaveChangesAsync();

            return mapper.Map<models.Product, ProductDto>(product);
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            try
            {
                var product = await GetProductById(productId);
                if (product == null)
                    return false;

                db.Remove(product);
                await db.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public async Task<ProductDto> GetProductById(int productId)
        {
            var product = await db.Products.FirstOrDefaultAsync(x => x.ProductId == productId);
            var productDto = mapper.Map<ProductDto>(product);
            return productDto;
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            IEnumerable<models.Product> products = await db.Products.ToListAsync();
            var productsListDto = mapper.Map<IEnumerable<ProductDto>>(products);
            return productsListDto;
        }
    }
}
