namespace Vk_clone.Services.AuthService.Types
{
    public class AuthResponseInfo
    {
        public string AccessToken { get; }
        public string RefreshToken { get; }

        public AuthResponseInfo(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}