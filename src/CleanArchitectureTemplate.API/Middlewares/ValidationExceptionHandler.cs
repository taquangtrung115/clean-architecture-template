using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureTemplate.API.Middlewares;

public class ValidationExceptionHandler(ILogger<ValidationExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        // check xem có đúng là ValidationException
        if (exception is not ValidationException validationException)
        {
            return false;
        }
        
        // ghi log lại 
        logger.LogError(validationException, "Exception occurred: {Message}", validationException.Message);
        
        var problemDetails = new ProblemDetails
        {
            Type = exception.GetType().Name,
            Status = StatusCodes.Status400BadRequest,
            Title = "Validation error",
            Detail = validationException.Message
        };
        
        /*nếu muốn custome thì lúc throw ra exception ở ValidationBehavior hãy truyền zo 
        if (exception.Errors is not null)
        {
            problemDetails.Extensions["errors"] = exception.Errors;
        }*/

        httpContext.Response.StatusCode = problemDetails.Status.Value;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        
        return true;
    }
}