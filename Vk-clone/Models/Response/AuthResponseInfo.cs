namespace Vk_clone.Errors.Request
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