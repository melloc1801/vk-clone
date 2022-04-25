using System.Threading.Tasks;
using Vk_clone.Errors.Request;

namespace Vk_clone.Errors.Request.Services.UserService
{
    public interface IUserService
    {
        Task<UserModel> CreateUser(SignupRequest signupRequest);
        Task<UserModel> ValidateUser(SigninRequest signinRequest);
    }
}