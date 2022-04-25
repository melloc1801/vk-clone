using System.ComponentModel.DataAnnotations;

namespace Vk_clone.Errors.Request
{
    public class SigninRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        [MinLength(2)]
        [MaxLength(64)]
        public string Password { get; set; }

        public SigninRequest(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}