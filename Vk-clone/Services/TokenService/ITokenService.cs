using System.Threading.Tasks;
using Vk_clone.Models;

namespace Vk_clone.Services.TokenService
{
    public interface ITokenService
    {
        (string accessToken, string refreshToken) RefreshTokens(TokenPayload tokenPayload);
        Task<(string accessToken, string refreshToken)> CreateTokens(TokenPayload tokenPayload);
        Task<(string accessToken, string refreshToken)> UpdateTokens(TokenPayload tokenPayload);
    }
}