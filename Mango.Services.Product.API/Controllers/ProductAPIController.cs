using Mango.Services.Product.API.Models.Dto;
using Mango.Services.Product.API.Repositry;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.Product.API.Controllers
{
    [Route("api/products")]
    public class ProductAPIController : ControllerBase
    {
        protected ResponseDto _response;
        private IProductRepositry _productRepositry;

        public ProductAPIController(IProductRepositry _productRepositry)
        {
            this._productRepositry = _productRepositry;
            _response = new ResponseDto();
        }

        [Authorize]
        [HttpGet]
        public async Task<object> Get()
        {
            try
            {
                IEnumerable<ProductDto> productDtos = await _productRepositry.GetProducts();
                _response.Result = productDtos;
            }
            catch (Exception e)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>()
                {
                    e.ToString()
                };
            }

            return _response;
        }

        [Authorize]
        [HttpGet]
        [Route("{id}")]
        public async Task<object> Get(int id)
        {
            try
            {
                ProductDto productDtos = await _productRepositry.GetProductById(id);
                _response.Result = productDtos;
            }
            catch (Exception e)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>()
                {
                    e.ToString()
                };
            }

            return _response;
        }

        [Authorize]
        [HttpPost]
        public async Task<object> Post([FromBody] ProductDto productDto)
        {
            try
            {
                ProductDto model = await _productRepositry.CreateUpdateProduct(productDto);
                _response.Result = model;
            }
            catch (Exception e)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>()
                {
                    e.ToString()
                };
            }

            return _response;
        }

        [Authorize]
        [HttpPut]
        public async Task<object> Update([FromBody] ProductDto productDto)
        {
            try
            {
                ProductDto model = await _productRepositry.CreateUpdateProduct(productDto);
                _response.Result = model;
            }
            catch (Exception e)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>()
                {
                    e.ToString()
                };
            }

            return _response;
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("{id}")]
        public async Task<object> Delete(int id)
        {
            try
            {
                _response.Result = await _productRepositry.DeleteProduct(id);
            }
            catch (Exception e)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>()
                {
                    e.ToString()
                };
            }

            return _response;
        }
    }
}
