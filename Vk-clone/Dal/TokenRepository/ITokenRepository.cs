using System.Threading.Tasks;

namespace Vk_clone.Dal.TokenRepository
{
    public interface ITokenRepository
    {
        Task<string> CreateRefreshToken(uint userId, string token);
        Task<string> UpdateRefreshToken(uint userId, string token);
        Task<uint> DeleteRefreshToken(uint userId);
    }
}