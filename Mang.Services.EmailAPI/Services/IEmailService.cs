using Mang.Services.EmailAPI.Models.Dto;
using Mang.Services.EmailAPI.Message;

namespace Mang.Services.EmailAPI.Services
{
    public interface IEmailService
    {
        Task EmailCart(CartDto cartDto);
        Task RegisterUserEmail(string email);
        Task LogOrderPlaced(RewardsMessage rewardsDto);
    }
}
