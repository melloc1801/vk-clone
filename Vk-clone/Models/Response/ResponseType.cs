using System.Linq;
using Vk_clone.Errors.Request;

namespace Vk_clone.Errors.Request
{
    public class ResponseType<T> 
        where T : class
    {
        public string Message { get; }
        public ErrorFieldResponse[] Errors { get; }
        public T Data { get; }

        private ResponseType(string message, ErrorFieldResponse[] errors, T data)
        {
            Message = message;
            Errors = errors;
            Data = data;
        }

        public static ResponseType<T> Create(T data)
        {
            return new ResponseType<T>(null, null, data);
        }
        public static ResponseType<T> Create(string message, ErrorFieldResponse[] errors)
        {
            return new ResponseType<T>(message, errors.ToArray(), null);
        }
    }
}