namespace Vk_clone.Models
{
    public class TokenPayload
    {
        public uint UserId { get; }
        public string Email { get; }

        public TokenPayload(uint userId, string email)
        {
            UserId = userId;
            Email = email;
        }
    }
}