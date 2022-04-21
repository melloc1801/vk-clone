using System.Threading.Tasks;
using Vk_clone.Dtos;
using Vk_clone.Models;

namespace Vk_clone.Services
{
    public interface IAuthService
    {
        Task<SignupResponse> SignUp(SignupDto signupDto);
        Task<SigninResponse> SignIn(SigninDto signinDto);
        void ConfirmSignUp();
        void ResetPassword();
        void Logout();
    }
}