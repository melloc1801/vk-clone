using System.Threading.Tasks;
using Vk_clone.Dtos;
using Vk_clone.Models;

namespace Vk_clone.Services
{
    public interface IUserService
    {
        Task<UserModel> CreateUser(CreateUserDto createUserDto);
    }
}