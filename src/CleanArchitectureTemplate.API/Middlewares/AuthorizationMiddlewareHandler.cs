using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureTemplate.API.Middlewares;

public class AuthorizationMiddlewareHandlerService : IAuthorizationMiddlewareResultHandler
{
    private readonly AuthorizationMiddlewareResultHandler _defaultHandler = new();
    public async Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
    {
        if(authorizeResult.Challenged)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            //response body
            var problemDetails = new ProblemDetails
            {
                Type = "Unauthorized",
                Status = StatusCodes.Status401Unauthorized,
                Title = "Unauthorized",
                Detail = "UnAuthorized: Access is Denied due invalid credential"
            };
            
            await context.Response.WriteAsJsonAsync(problemDetails);
            return;
        }
        if (authorizeResult.Forbidden)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            //response body
            var problemDetails = new ProblemDetails
            {
                Type = "Forbidden",
                Status = StatusCodes.Status403Forbidden,
                Title = "Forbidden",
                Detail = "Permission: You do not have permission to access this resource"
            };
            await context.Response.WriteAsJsonAsync(problemDetails);
            return;
        }
        await _defaultHandler.HandleAsync(next, context, policy, authorizeResult);
    }
}