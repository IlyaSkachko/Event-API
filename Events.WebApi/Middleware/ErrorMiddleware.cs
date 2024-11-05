using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace Events.WebApi.Middleware
{
    public class ErrorMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleExeptionAsync(context, ex);
            }
        }

        private async Task HandleExeptionAsync(HttpContext context, Exception exception)
        {
            var problemDetails = new ProblemDetails();

            problemDetails.Status = (int)HttpStatusCode.BadRequest;
            problemDetails.Title = exception.Message;

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = problemDetails.Status.Value;

            var jsonProblemDetails = JsonSerializer.Serialize(problemDetails);
            
            await context.Response.WriteAsync(jsonProblemDetails);
        }
    }
}
