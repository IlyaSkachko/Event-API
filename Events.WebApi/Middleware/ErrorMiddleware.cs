using Events.Application.Exceptions;
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
            catch (Exception exeption)
            {
                await HandleExeptionAsync(context, exeption);
            }
        }

        private async Task HandleExeptionAsync(HttpContext context, Exception exception)
        {
            var problemDetails = new ProblemDetails();

            switch (exception)
            {
                case NotFoundException:
                    problemDetails.Status = (int)HttpStatusCode.NotFound;
                    problemDetails.Title = "Resource not found";
                    break;
                case AlreadyExistException:
                    problemDetails.Status = (int)HttpStatusCode.Conflict;
                    problemDetails.Title = "Resource already exists";
                    break;
                case BadRequestException:
                    problemDetails.Status = (int)HttpStatusCode.BadRequest;
                    problemDetails.Title = "Bad request";
                    break;
                case UnauthorizedAccessException:
                    problemDetails.Status = (int)HttpStatusCode.Unauthorized;
                    problemDetails.Title = "Unauthorized";
                    break;
                default:
                    problemDetails.Status = (int)HttpStatusCode.InternalServerError;
                    problemDetails.Title = "An unexpected error occurred";
                    break;
            }

            problemDetails.Detail = exception.Message;

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = problemDetails.Status.Value;

            var jsonProblemDetails = JsonSerializer.Serialize(problemDetails);

            await context.Response.WriteAsync(jsonProblemDetails);
        }
    }
}
