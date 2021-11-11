using RideShare.Application.Common;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;
using System;

namespace RideShare.Infrastructure.Filters
{
    public class CustomExceptionFilter :ExceptionFilterAttribute
    {
        public async override Task OnExceptionAsync(ExceptionContext context)
        {
            int statusCode = (int)HttpStatusCode.InternalServerError;
            if (context.Exception is NullReferenceException)
                statusCode = (int)HttpStatusCode.NotFound;

            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = statusCode;
            context.Result = new ObjectResult(new {
                errors = new[] { context.Exception.Message },
                source = context.Exception.Source
            });

            await Task.CompletedTask;
        }
    }
}