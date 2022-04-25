using System;

namespace Vk_clone.Errors
{
    public class IncorrectPasswordError: Exception
    {
        public IncorrectPasswordError()
            : base(message: "Incorrect password")
        { }
    }
}