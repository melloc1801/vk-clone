using System.Threading.Tasks;
using Vk_clone.Errors.Request;

namespace Vk_clone.Errors.Request.Services.AuthService
{
    public interface IAuthService
    {
        Task<ResponseType<AuthResponseInfo>> SignUp(SignupRequest signupRequest);
        Task<ResponseType<AuthResponseInfo>> SignIn(SigninRequest signinRequest);
        void ConfirmSignUp();
        void ResetPassword();
        void Logout();
    }
}