using AutoMapper;
using Mango.Services.ShoppimgCartAPI.DbContexts;
using Mango.Services.ShoppimgCartAPI.Models;
using Mango.Services.ShoppimgCartAPI.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ShoppimgCartAPI.Repositry
{
    public class CartRepositry : ICartRepositry
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;

        public CartRepositry(ApplicationDbContext _db, IMapper _mapper)
        {
            db = _db;
            mapper = _mapper;
        }

        public async Task<bool> ClearCart(string userId)
        {
            var cartHeaderFromDb = await db.CartHeaders.FirstOrDefaultAsync(u => u.UserId == userId);
            if (cartHeaderFromDb != null)
            {
                db.CartDetails
                    .RemoveRange(db.CartDetails.Where(u => u.CartHeaderId == cartHeaderFromDb.CartHeaderId));

                db.CartHeaders.Remove(cartHeaderFromDb);
                await db.SaveChangesAsync();

                return true;
            }
            return false;
        }

        public async Task<CartDto> CreateUpdateCart(CartDto cartDto)
        {
            Cart cart = mapper.Map<Cart>(cartDto);
            var prodInDb = await db.Products.FirstOrDefaultAsync(u => u.ProductId ==
                cartDto.CartDetails.FirstOrDefault().ProductId);

            if (prodInDb == null)
            {
                db.Products.Add(cart.CartDetails.First().Product);
                await db.SaveChangesAsync();
            }

            var cartHeaderFromDb = await db.CartHeaders.AsNoTracking()
                .FirstOrDefaultAsync(u => u.UserId == cart.CartHeader.UserId);

            if (cartHeaderFromDb == null)
            {
                db.CartHeaders.Add(cart.CartHeader);
                await db.SaveChangesAsync();
                cart.CartDetails.FirstOrDefault().CartHeaderId = cart.CartHeader.CartHeaderId;

                cart.CartDetails.FirstOrDefault().Product = null;
                db.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                await db.SaveChangesAsync();
            }
            else
            {
                var cartDetailsFromDb = await db.CartDetails.AsNoTracking()
                    .FirstOrDefaultAsync(u => u.ProductId == cart.CartDetails.FirstOrDefault().ProductId &&
                    u.CartHeaderId == cartHeaderFromDb.CartHeaderId);

                if (cartDetailsFromDb == null)
                {
                    cart.CartDetails.FirstOrDefault().CartHeaderId = cartDetailsFromDb.CartHeader.CartHeaderId;
                    cart.CartDetails.FirstOrDefault().Product = null;
                    db.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                    await db.SaveChangesAsync();
                }
                else
                {
                    cart.CartDetails.FirstOrDefault().Count += cartDetailsFromDb.Count;
                    db.CartDetails.Update(cart.CartDetails.FirstOrDefault());
                    await db.SaveChangesAsync();
                }
            }

            return mapper.Map<CartDto>(cart);

        }

        public async Task<CartDto> GetCartByUserId(string userId)
        {
            Cart cart = new Cart()
            {
                CartHeader = await db.CartHeaders.FirstOrDefaultAsync(u => u.UserId == userId)
            };

            cart.CartDetails = db.CartDetails
                .Where(u => u.CartHeaderId == cart.CartHeader.CartHeaderId)
                .Include(u => u.Product);
            return mapper.Map<CartDto>(cart);
        }

        public async Task<bool> ApplyCoupon(string userId, string couponCode)
        {
            var cartFromDb = await db.CartHeaders.FirstOrDefaultAsync(u => u.UserId == userId);
            cartFromDb.CouponCode = couponCode;
            db.CartHeaders.Update(cartFromDb);

            await db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveCoupon(string userId)
        {
            var cartFromDb = await db.CartHeaders.FirstOrDefaultAsync(u => u.UserId == userId);
            cartFromDb.CouponCode = "";
            db.CartHeaders.Update(cartFromDb);

            await db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveFromCart(int cartDetailsId)
        {
            try
            {
                CartDetails cartDetails = await db.CartDetails
                    .FirstOrDefaultAsync(u => u.CartDetailsId == cartDetailsId);

                int totalCountOfCartItems = db.CartDetails
                    .Where(u => u.CartHeaderId == cartDetails.CartHeaderId)
                    .Count();

                db.CartDetails.Remove(cartDetails);
                if (totalCountOfCartItems == 1)
                {
                    var cartHeaderToRemove = await db.CartHeaders.
                        FirstOrDefaultAsync(u => u.CartHeaderId == cartDetails.CartHeaderId);
                    db.CartHeaders.Remove(cartHeaderToRemove);
                }

                await db.SaveChangesAsync();
            }
            catch (Exception ex) 
            {
                return false;
            }
            return true;
        }
    }
}
