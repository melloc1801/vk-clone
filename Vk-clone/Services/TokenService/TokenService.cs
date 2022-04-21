using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Vk_clone.Dal.TokenRepository;
using Vk_clone.Models;

namespace Vk_clone.Services
{
    public class TokenService: ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly ITokenRepository _tokenRepository;

        public TokenService(IConfiguration configuration, ITokenRepository tokenRepository)
        {
            _configuration = configuration;
            _tokenRepository = tokenRepository;
        }
        
        private bool ValidateToken(string token, string secretKey)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            try
            {
                tokenHandler.ValidateToken(token, 
                    new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuerSigningKey = true,
                    }, 
                    out SecurityToken validatedToken
                );
        
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }
        private string GenerateAccessToken(TokenPayload tokenPayload)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:accessTokenSecretKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", tokenPayload.UserId.ToString()), 
                    new Claim("email", tokenPayload.Email)
                }),
                Expires = DateTime.UtcNow.AddMinutes(_configuration
                    .GetSection("Jwt")
                    .GetValue<int>("accessTokenExpiresInMunutes")
                ),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        private string GenerateRefreshToken(TokenPayload tokenPayload)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:refreshTokenSecretKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", tokenPayload.UserId.ToString()), 
                    new Claim("email", tokenPayload.Email)
                }),
                Expires = DateTime.UtcNow.AddDays(_configuration
                    .GetSection("Jwt")
                    .GetValue<int>("refreshTokenExpiresInDays")
                ),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public async Task<(string accessToken, string refreshToken)> CreateTokens(TokenPayload tokenPayload)
        {
            var accessToken = GenerateAccessToken(tokenPayload);
            var refreshToken = await _tokenRepository.CreateRefreshToken(tokenPayload.UserId, GenerateRefreshToken(tokenPayload));
            return (accessToken, refreshToken);
        }

        public async Task<(string accessToken, string refreshToken)> UpdateTokens(TokenPayload tokenPayload)
        {
            var accessToken = GenerateAccessToken(tokenPayload);
            var refreshToken = await _tokenRepository.UpdateRefreshToken(tokenPayload.UserId, GenerateRefreshToken(tokenPayload));
            return (accessToken, refreshToken);
        }

        public (string accessToken, string refreshToken) RefreshTokens(TokenPayload tokenPayload)
        {
            var accessToken = GenerateAccessToken(tokenPayload);
            var refreshToken = GenerateRefreshToken(tokenPayload);
            
            return (accessToken, refreshToken);
        }

        public void ValidateRefreshToken(string token)
        {
            ValidateToken(token, _configuration["Jwt:refreshTokenSecretKey"]);
        }
    }
}