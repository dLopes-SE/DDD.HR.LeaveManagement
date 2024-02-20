using Api.Models;
using Application.Exceptions;
using System.Net;

namespace Api.Middleware
{
  public class ExceptionMiddleware(RequestDelegate next)
  {
    public async Task InvokeAsync(HttpContext context)
    {
      try
      {
        await next(context);
      }
      catch (Exception ex)
      {

        await HandleExceptionAsync(context, ex);
      }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
      var statusCode = HttpStatusCode.InternalServerError;
      CustomProblemDetails problem = new();

      switch (ex)
      {
        case BadRequestException badRequestException:
          statusCode = HttpStatusCode.BadRequest;
          problem = new CustomProblemDetails
          {
            Title = ex.Message,
            Status = (int)statusCode,
            Type = nameof(BadRequestException),
            Detail = ex.InnerException?.Message,
            Errors = badRequestException.ValidationErrors
          };
          break;
        case NotFoundException notFound:
          statusCode = HttpStatusCode.NotFound;
          problem = new CustomProblemDetails
          {
            Title = ex.Message,
            Status = (int)statusCode,
            Type = nameof(notFound),
            Detail = ex.InnerException?.Message
          };
          break;
        default:
          problem = new CustomProblemDetails
          {
            Title = ex.Message,
            Status = (int)statusCode,
            Type = nameof(HttpStatusCode.InternalServerError),
            Detail = ex.StackTrace
          };
          break;
      }

      context.Response.StatusCode = (int)statusCode;
      await context.Response.WriteAsJsonAsync(problem);
    }
  }
}
