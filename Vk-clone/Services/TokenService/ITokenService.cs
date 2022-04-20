using Vk_clone.Models;

namespace Vk_clone.Services
{
    public interface ITokenService
    {
        (string accessToken, string refreshToken) RefreshTokens(TokenPayload tokenPayload);
        (string accessToken, string refreshToken) CreateToken(TokenPayload tokenPayload);
    }
}