using System.Threading.Tasks;
using Vk_clone.Dtos;
using Vk_clone.Models;

namespace Vk_clone.Services
{
    public interface IAuthService
    {
        Task<SignUpResponse> SignUp(CreateUserDto createUserDto);
        void SignIn();
        void ConfirmSignUp();
        void ResetPassword();
        void Logout();
    }
}