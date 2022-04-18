namespace Vk_clone.Models
{
    public class SignUpResponse
    {
        public string Email { get; }
        public string AccessToken { get; }
        public string RefreshToken { get; }

        public SignUpResponse(string email, string accessToken, string refreshToken)
        {
            Email = email;
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}