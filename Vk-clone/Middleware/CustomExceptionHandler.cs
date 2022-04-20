using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Vk_clone.Models;

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
            var code = HttpStatusCode.InternalServerError;
            string message = exception.Message;
            string errorCode = HttpStatusCode.InternalServerError.ToString();

            if ((ErrorCodes)exception.Data["ErrorCode"] == ErrorCodes.EmailAlreadyExists)
            {
                errorCode = ErrorCodes.EmailAlreadyExists.ToString();
                code = HttpStatusCode.OK;
            }
            
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            
            var result = JsonSerializer.Serialize(new ErrorResponseType(message, errorCode));
            return context.Response.WriteAsync(result);
        }
    }
}