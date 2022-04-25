using System.Threading.Tasks;
using Vk_clone.Errors.Request;

namespace Vk_clone.Errors.Request.Dal.UserRepository
{
    public interface IUserRepository
    {
        Task<UserModel> FindOneByEmail(string email);
        Task<UserModel> CreateUser(SignupRequest signupRequest);
        void GetUser();
        
    }
}