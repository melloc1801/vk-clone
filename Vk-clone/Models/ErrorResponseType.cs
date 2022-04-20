namespace Vk_clone.Models
{
    public class ErrorResponseType
    {
        public string Message { get; }
        public string ErrorCode { get; }

        public ErrorResponseType(string message, string errorCode)
        {
            Message = message;
            ErrorCode = errorCode;
        }
    }
}