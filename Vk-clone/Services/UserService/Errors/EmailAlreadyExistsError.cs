using System;

namespace Vk_clone.Services.UserService.Errors
{
    public class EmailAlreadyExistsError: Exception
    {
        public EmailAlreadyExistsError(string email)
            : base(message: $"User with {email} already exists")
        { }
    }
}