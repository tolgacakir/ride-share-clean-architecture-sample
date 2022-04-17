using RideShare.Application.Common;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;
using System;
using FluentValidation;

namespace RideShare.Infrastructure.Filters
{
    public class CustomExceptionFilter :ExceptionFilterAttribute
    { 
        public async override Task OnExceptionAsync(ExceptionContext context)
        {
            int statusCode;
            
            if (context.Exception is NullReferenceException or ArgumentNullException)
            {
                statusCode = (int)HttpStatusCode.NotFound;
            }
            else if (context.Exception is ArgumentException or ArgumentOutOfRangeException or ValidationException or InvalidOperationException)
            {
                statusCode = (int)HttpStatusCode.BadRequest;   
            }
            else
            {
                statusCode = (int)HttpStatusCode.InternalServerError;
            }

            context.HttpContext.Response.StatusCode = statusCode;
            context.HttpContext.Response.ContentType = "application/json";
            
            context.Result = new ObjectResult(new {
                Errors = new[] { context.Exception.Message },
                Source = context.Exception.Source,
            });

            await Task.CompletedTask;
        }
    }
}