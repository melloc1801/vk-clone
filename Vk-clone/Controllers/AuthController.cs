using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vk_clone.Dtos;
using Vk_clone.Models;
using Vk_clone.Services;

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
        public async Task<SignupResponse> SignUp([FromBody] SignupDto signupDto)
        {
            return await _authService.SignUp(signupDto);
        }

        [HttpPost("/signin")]
        public async Task<SigninResponse> SignIn([FromBody] SigninDto signinDto)
        {
            return await _authService.SignIn(signinDto);
        }
    }
}