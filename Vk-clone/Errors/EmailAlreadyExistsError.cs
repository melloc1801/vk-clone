using System;

namespace Vk_clone.Errors
{
    public class EmailAlreadyExistsError: Exception
    {
        public EmailAlreadyExistsError(string email)
            : base(message: $"User with {email} already exists")
        { }
    }
}