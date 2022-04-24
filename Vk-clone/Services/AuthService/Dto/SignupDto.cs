using System.ComponentModel.DataAnnotations;

namespace Vk_clone.Services.AuthService.Dto
{
    public class SignupDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        [MinLength(2)]
        [MaxLength(64)]
        public string Password { get; set; }

        public SignupDto(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}