using Mang.Services.AuthAPI.Data;
using Mang.Services.AuthAPI.Models;
using Mang.Services.AuthAPI.Models.Dto;
using Mang.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Identity;

namespace Mang.Services.AuthAPI.Service
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(AppDbContext db,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<bool> AssignRole(string email, string roleName)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(x => x.Email.ToLower() == email.ToLower());
            if (user == null)
            {
                if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                }
                await _userManager.AddToRoleAsync(user, roleName);
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(x => x.UserName.ToLower() == loginRequestDto.UserName.ToLower());
            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

            if (user == null || isValid == false)
            {
                return new LoginResponseDto() { User = null, Token = "" };
            }

            // Generat JWT Token

            var token = _jwtTokenGenerator.GenerateToken(user);

            UserDto userDto = new UserDto()
            {
                ID = user.Id,
                Name = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            };

            LoginResponseDto loginResponseDto = new LoginResponseDto()
            {
                User = userDto,
                Token = token
            };

            return loginResponseDto;

        }




        public async Task<string> Register(RegistretionRequestDto registretionRequestDto)
        {
            ApplicationUser user = new ApplicationUser()
            {
                UserName = registretionRequestDto.Email,
                Email = registretionRequestDto.Email,
                Name = registretionRequestDto.Name,
                PhoneNumber = registretionRequestDto.PhoneNumber,
                NormalizedEmail = registretionRequestDto.Email.ToUpper(),
            };
            try
            {
                var result = await _userManager.CreateAsync(user, registretionRequestDto.Password);
                if (result.Succeeded)
                {
                    var userToReturn = _db.ApplicationUsers.First(x => x.UserName == registretionRequestDto.Email);

                    UserDto userDto = new UserDto()
                    {
                        ID = userToReturn.Id,
                        Name = userToReturn.Name,
                        Email = userToReturn.Email,
                        PhoneNumber = userToReturn.PhoneNumber,
                    };

                    return "";
                }
                else 
                {
                    return result.Errors.FirstOrDefault().Description;
                }

            }
            catch (Exception ex)
            {
                
            }
            return "Erorr Encounted";
        }
    }
}
