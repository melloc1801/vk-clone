namespace Vk_clone.Errors.Request
{
    public class ErrorFieldResponse
    {
        public string Message { get; }
        public string Field { get; }

        public ErrorFieldResponse(string message, string field)
        {
            Message = message;
            Field = field;
        }
    }
}