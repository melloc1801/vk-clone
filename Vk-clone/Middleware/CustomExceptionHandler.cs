using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;
using Vk_clone.Errors.Request;

namespace Vk_clone.Errors.Request.Middleware
{
    public class CustomExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerFactory _loggerFactory;

        public CustomExceptionHandler(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _loggerFactory = loggerFactory;
        }

        public async Task Invoke(HttpContext context)
        {
            var logger = _loggerFactory.CreateLogger<CustomExceptionHandler>();
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                logger.LogError(exception, exception.Message);
                await HandleExceptionAsync(context, exception);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var result = JsonSerializer.Serialize(ResponseType<object>.Create(exception.Message, null));
            return context.Response.WriteAsync(result);
        }
    }
}