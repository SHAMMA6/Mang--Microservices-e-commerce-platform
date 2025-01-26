using Mang.Services.ShoppingCartAPI.Models.Dto;

namespace Mang.Services.ShoppingCartAPI.Service.IService
{
    public interface ICouponService
    {
        Task<CouponDto> GetCoupon(string couponCode);
    }
}
