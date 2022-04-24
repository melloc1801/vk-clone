using System;

namespace Vk_clone.Services.UserService.Errors
{
    public class IncorrectPasswordError: Exception
    {
        public IncorrectPasswordError()
            : base(message: "Incorrect password")
        { }
    }
}