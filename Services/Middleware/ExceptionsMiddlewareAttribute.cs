
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading;
using Xunit.Sdk;

namespace Services.Middleware
{
    public class ExceptionsMiddlewareAttribute : ExceptionFilterAttribute
    {
        override
        public void OnException(ExceptionContext context)
        {
            // check if exception is an argument exception/status 400 error (made in BaseController)
            if (context.Exception is ArgumentException ex)
            {
                context.Result = new BadRequestObjectResult(ex.Message);
                context.ExceptionHandled = true;
            }
            // check if exception is null, give a status 404 error
            else if (context.Exception is NullException)
            {
                context.Result = new NotFoundObjectResult("Status 404 error.");
                context.ExceptionHandled = true;
            }
            // handle InvalidOperationException
            else if (context.Exception is InvalidOperationException)
            {
                context.Result = new ObjectResult("Sequence contains no elements. Enter a different range.")
                {
                    StatusCode = 400
                };
                context.ExceptionHandled = true;
            }
            // handle FormatException
            else if (context.Exception is FormatException)
            {
                context.Result = new BadRequestObjectResult(context.Exception.Message);
                context.ExceptionHandled = true;
            }
            else
            {
                context.Result = new ObjectResult("Sorry something went wrong. Status 500 error.")
                {
                    StatusCode = 500
                };
                context.ExceptionHandled = true;
            }
        }
    }
}
