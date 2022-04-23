using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Vk_clone.Services.AuthService.Types;
using Vk_clone.Types;

namespace Vk_clone.Middleware
{
    public class CustomExceptionHandler
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {   
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            
            var result = JsonSerializer.Serialize(ResponseType<AuthResponseInfo>.Create(new []{exception.Message}));
            return context.Response.WriteAsync(result);
        }
    }
}