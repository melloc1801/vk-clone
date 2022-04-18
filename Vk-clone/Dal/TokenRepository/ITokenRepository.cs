using System.Threading.Tasks;

namespace Vk_clone.Dal.TokenRepository
{
    public interface ITokenRepository
    {
        Task<string> CreateRefreshToken(string token, uint userId);
        Task<string> UpdateRefreshToken(uint id, string token);
        Task<uint> DeleteRefreshToken(uint id);
    }
}