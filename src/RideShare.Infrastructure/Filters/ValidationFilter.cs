
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RideShare.Infrastructure.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new ObjectResult(new {
                    Errors = context.ModelState
                        .Where(x=>x.Value.Errors.Count > 0)
                        .SelectMany(y=>y.Value.Errors)
                        .Select(z=>z.ErrorMessage),
                    Source = "ValidationFilter"
                });
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return;
            }
            await next();
        }
    }
}