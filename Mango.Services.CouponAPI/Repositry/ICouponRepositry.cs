using Mango.Services.CouponAPI.Models.Dtos;

namespace Mango.Services.CouponAPI.Repositry
{
    public interface ICouponRepositry
    {
        Task<CouponDto> GetCouponByCode(string couponCode);
    }
}
