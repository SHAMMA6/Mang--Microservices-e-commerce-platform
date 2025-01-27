using Mang.Services.EmailAPI.Models.Dto;

namespace Mang.Services.EmailAPI.Services
{
    public interface IEmailService
    {
        Task EmailCart(CartDto cartDto);
        Task RegisterUserEmail(string email);
        Task LogOrderPlaced(RewardsMessage rewardsDto);
    }
}
