using System.Threading.Tasks;
using Vk_clone.Models;
using Vk_clone.Services.AuthService.Dto;

namespace Vk_clone.Dal.UserRepository
{
    public interface IUserRepository
    {
        Task<UserModel> FindOneByEmail(string email);
        Task<UserModel> CreateUser(SignupDto signupDto);
        void GetUser();
        
    }
}