using Mang.Services.AuthAPI.Models.Dto;

namespace Mang.Services.AuthAPI.Service.IService
{
    public interface IAuthService
    {
        Task<string> Register(RegistretionRequestDto registretionRequestDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<bool>  AssignRole(string email, string roleName);
    }
}
