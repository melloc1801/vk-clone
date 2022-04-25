using System;

namespace Vk_clone.Errors
{
    public class UserWithEmailNotFoundError : Exception
    {
        public UserWithEmailNotFoundError(string email)
            : base(message: $"User with {email} not found")
        {
            
        }
    }
}