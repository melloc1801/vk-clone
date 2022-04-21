namespace Vk_clone.Models
{
    public class SignupResponse
    {
        public string Email { get; }
        public string AccessToken { get; }
        public string RefreshToken { get; }

        public SignupResponse(string email, string accessToken, string refreshToken)
        {
            Email = email;
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}