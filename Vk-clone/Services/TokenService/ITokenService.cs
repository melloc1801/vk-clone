using System;
using Vk_clone.Models;

namespace Vk_clone.Services
{
    public interface ITokenService
    {
        bool ValidateToken(string token, string secretKey);
        string GenerateAccessToken(TokenPayload tokenPayload);
        string GenerateRefreshToken(TokenPayload tokenPayload);
        (string accessToken, string refreshToken) RefreshTokens(TokenPayload tokenPayload);
    }
}