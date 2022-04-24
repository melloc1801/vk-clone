using System.Linq;

namespace Vk_clone.Types
{
    public class ResponseType<T> 
        where T : class
    {
        public string[] ErrorCodes { get; }
        public T Data { get; }

        private ResponseType(string[] errorCodes, T data)
        {
            ErrorCodes = errorCodes;
            Data = data;
        }

        public static ResponseType<T> Create(T data)
        {
            return new ResponseType<T>(null, data);
        }
        public static ResponseType<T> Create(string[] errorCodes)
        {
            return new ResponseType<T>(errorCodes.ToArray(), null);
        }
    }
}