using System.ComponentModel.DataAnnotations;

namespace Vk_clone.Dtos
{
    public class SigninDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        [MinLength(2)]
        [MaxLength(64)]
        public string Password { get; set; }

        public SigninDto(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}