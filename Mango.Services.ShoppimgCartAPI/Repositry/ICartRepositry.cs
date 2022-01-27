using Mango.Services.ShoppimgCartAPI.Models.Dto;

namespace Mango.Services.ShoppimgCartAPI.Repositry
{
    public interface ICartRepositry
    {
        Task<CartDto> GetCartByUserId(string userId);
        Task<CartDto> CreateUpdateCart(CartDto cartDto);
        Task<bool> RemoveFromCart(int cartDetailsId);
        Task<bool> ClearCart(string userId);

        Task<bool> ApplyCoupon(string userId, string couponCode);
        Task<bool> RemoveCoupon(string userId);
    }
}
