using ExceptionHandlingInMiddleware.Exceptions;
using System.Net;
using System.Text.Json;

namespace ExceptionHandlingInMiddleware.Configurations;

public class ExceptionMiddlewareConfiguration
{
    private readonly RequestDelegate _next;
    public ExceptionMiddlewareConfiguration(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception excp)
        {
            await ExceptionAsync(context, excp);
        }
    }

    private static Task ExceptionAsync(HttpContext context, Exception ex)
    {
        // here the httpcodes will be determined based on exceptions
        HttpStatusCode statusCode;
        string message = "Unexpected error";

        // we need to find out the type of the exception
        var excpType = ex.GetType();

        // let's check what kind of exceptions passed
        if (excpType == typeof(BadRequestException))
        {
            statusCode = HttpStatusCode.BadRequest;
            message = ex.Message;
        }
        else if (excpType == typeof(NotFoundException))
        {
            statusCode = HttpStatusCode.NotFound;
            message = ex.Message;
        }
        else if (excpType == typeof(Exceptions.NotImplementedException))
        {
            statusCode = HttpStatusCode.NotImplemented;
            message = ex.Message;
        }
        else if (excpType == typeof(UnauthorizedException))
        {
            statusCode = HttpStatusCode.Unauthorized;
            message = ex.Message;
        }
        else
        {
            statusCode = HttpStatusCode.InternalServerError;
            message = ex.Message;
        }

        var result = JsonSerializer.Serialize(new { message = message });
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        return context.Response.WriteAsync(result);

    }
}
