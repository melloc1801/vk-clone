using System;
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
        public async Task<SignUpResponse> SignUp([FromBody] CreateUserDto createUserDto)
        {
            return await _authService.SignUp(createUserDto);
        }
    }
}