using System.ComponentModel.DataAnnotations;

namespace Vk_clone.Errors.Request
{
    public class UserModel
    {
        [Required]
        public uint Id { get; }
        
        [Required]
        [EmailAddress]
        [MaxLength(64)]
        public string Email { get; }
        
        [Required]
        [MinLength(2)]
        [MaxLength(64)]
        public string Password { get; }

        public UserModel(uint id, string email, string password)
        {
            Id = id;
            Email = email;
            Password = password;
        }
    }
}