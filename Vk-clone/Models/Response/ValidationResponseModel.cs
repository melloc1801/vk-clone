using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Vk_clone.Errors.Request
{
    public class ValidationResponseModel
    {
        public string Message { get; }
        public List<ErrorFieldResponse> Errors { get; }
        public ValidationResponseModel(ModelStateDictionary modelState)
        {
            Message = "Validation error";
            Errors = modelState.Keys
                .SelectMany(key => modelState[key].Errors.Select(x => new ErrorFieldResponse(key,x.ErrorMessage)))
                .ToList();
        }
    }
}