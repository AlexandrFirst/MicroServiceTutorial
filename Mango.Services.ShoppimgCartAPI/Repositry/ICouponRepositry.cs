using Mango.Services.ShoppimgCartAPI.Models.Dto;

namespace Mango.Services.ShoppimgCartAPI.Repositry
{
    public interface ICouponRepositry
    {
        Task<CouponDto> GetCoupon(string couponName);
    }
}
