using System.Threading.Tasks;
using Vk_clone.Services.AuthService.Dto;
using Vk_clone.Services.AuthService.Types;
using Vk_clone.Types;

namespace Vk_clone.Services.AuthService
{
    public interface IAuthService
    {
        Task<ResponseType<AuthResponseInfo>> SignUp(SignupDto signupDto);
        Task<ResponseType<AuthResponseInfo>> SignIn(SigninDto signinDto);
        void ConfirmSignUp();
        void ResetPassword();
        void Logout();
    }
}