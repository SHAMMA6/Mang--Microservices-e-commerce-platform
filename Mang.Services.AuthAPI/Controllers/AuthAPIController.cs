using Mang.Services.AuthAPI.Models.Dto;
using Mang.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Mang.Services.AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        private readonly IAuthService _authService;
        protected ResponseDto _response;

        public AuthAPIController(IAuthService authService)
        {
            _authService = authService;
            _response = new();
        }



        [HttpGet("register")]
        public async Task<IActionResult> Register([FromBody]RegistretionRequestDto model)
        {
            var errorMesseg = await _authService.Register(model);
            if (!string.IsNullOrEmpty(errorMesseg))
            {
                _response.IsSuccess = false;
                _response.Message = errorMesseg;
                return BadRequest(_response);
            }
            return Ok(_response);
        }

        [HttpGet("login")]
        public async Task<IActionResult> Login([FromBody]LoginRequestDto model)
        {
            var loginResponse = await _authService.Login(model);
            if (loginResponse == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Invalid login attempt";
                return BadRequest(_response);
            }
            _response.Resulte = loginResponse;
            return Ok(_response);
        }

        [HttpGet("AssignRole")]
        public async Task<IActionResult> AssignRole([FromBody] RegistretionRequestDto model)
        {
            var assignRoleSuccessful = await _authService.AssignRole(model.Email,model.Role);
            if (!assignRoleSuccessful)
            {
                _response.IsSuccess = false;
                _response.Message = "Erorr Encounted";
                return BadRequest(_response);
            }
            
            return Ok(_response);
        }
    }
}
