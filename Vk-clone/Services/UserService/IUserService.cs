using System.Threading.Tasks;
using Vk_clone.Models;
using Vk_clone.Services.AuthService.Dto;

namespace Vk_clone.Services.UserService
{
    public interface IUserService
    {
        Task<UserModel> CreateUser(SignupDto signupDto);
        Task<UserModel> ValidateUser(SigninDto signinDto);
    }
}