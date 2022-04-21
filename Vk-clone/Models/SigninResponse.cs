namespace Vk_clone.Models
{
    public class SigninResponse
    {
        public string Email { get; }
        public string AccessToken { get; }
        public string RefreshToken { get; }

        public SigninResponse(string email, string accessToken, string refreshToken)
        {
            Email = email;
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}