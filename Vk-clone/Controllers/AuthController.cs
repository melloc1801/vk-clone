using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vk_clone.Errors.Request.Services.AuthService;
using Vk_clone.Errors.Request;

namespace Vk_clone.Errors.Request.Controllers
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
        public async Task<ResponseType<AuthResponseInfo>> SignUp(SignupRequest signupRequest)
        {
            return await _authService.SignUp(signupRequest);
        }

        [HttpPost("/signin")]
        public async Task<ResponseType<AuthResponseInfo>> SignIn([FromBody] SigninRequest signinRequest)
        {
            return await _authService.SignIn(signinRequest);
        }
    }
}