using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vk_clone.Services.AuthService;
using Vk_clone.Services.AuthService.Dto;
using Vk_clone.Services.AuthService.Types;
using Vk_clone.Types;

namespace Vk_clone.Controllers
{
    [ApiController]
    [Route("/auth")]
    public class UserController : ControllerBase
    {
        private readonly IAuthService _authService;
        
        public UserController(IAuthService authService)
        {
            _authService = authService;
        }
        
        [HttpPost("/signup")]
        public async Task<ResponseType<AuthResponseInfo>> SignUp([FromBody] SignupDto signupDto)
        {
            return await _authService.SignUp(signupDto);
        }

        [HttpPost("/signin")]
        public async Task<ResponseType<AuthResponseInfo>> SignIn([FromBody] SigninDto signinDto)
        {
            return await _authService.SignIn(signinDto);
        }
    }
}