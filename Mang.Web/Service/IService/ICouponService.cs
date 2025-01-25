using Mang.Web.Models;

namespace Mang.Web.Service.IService
{
    public interface ICouponService
    {
        Task<ResponseDto?> GetCouponAsync(string couponCode);
        Task<ResponseDto?> GetCouponByIdAsync(int id);
        Task<ResponseDto?> GetAllCouponsAsync();
        Task<ResponseDto?> CreatCouponsAsync(CouponDto couponDto);
        Task<ResponseDto?> UbdateCouponsAsync(CouponDto couponDto);
        Task<ResponseDto?> DeleteCouponsAsync(int id);
    }
}
