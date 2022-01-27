using Mango.Services.CouponAPI.Models.Dtos;
using Mango.Services.CouponAPI.Repositry;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.Controllers
{
    [ApiController]
    [Route("api/coupon")]
    public class CouponAPIController : Controller
    {
        private readonly ICouponRepositry _couponRepositry;
        protected ResponseDto _response;

        public CouponAPIController(ICouponRepositry couponRepositry)
        {
            _couponRepositry = couponRepositry;
            _response = new ResponseDto();
        }

        [HttpGet("{code}")]
        public async Task<object> GetDiscountForCode(string code) 
        {
            try
            {
                var coupon = await _couponRepositry.GetCouponByCode(code);
                _response.Result = coupon;
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
    }
}
