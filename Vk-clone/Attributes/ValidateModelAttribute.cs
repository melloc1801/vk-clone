using Microsoft.AspNetCore.Mvc.Filters;
using Vk_clone.Errors.Response;

namespace Vk_clone.Errors.Request.Attributes
{
    public class ValidateModelAttribute: ActionFilterAttribute
    { 
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new ValidationFailedResponse(context.ModelState);
            }
        }
    }
}