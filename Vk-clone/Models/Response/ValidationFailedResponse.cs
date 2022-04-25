using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Vk_clone.Errors.Request;

namespace Vk_clone.Errors.Response
{
    public class ValidationFailedResponse: ObjectResult
    {
        public ValidationFailedResponse(ModelStateDictionary modelState)
            : base(new ValidationResponseModel(modelState))
        {
            StatusCode = StatusCodes.Status422UnprocessableEntity; //change the http status code to 422.
        }
    }
}