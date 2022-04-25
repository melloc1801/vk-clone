using Microsoft.AspNetCore.Builder;

namespace Vk_clone.Errors.Request.Middleware.CustomExceptionHandler
{
    public static class CustomExceptionHandlerExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandler>();
        }
    }
}