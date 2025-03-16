using CleanArchitectureTemplate.Application.Behaviors;
using CleanArchitectureTemplate.Application.ClaimUser;
using FluentValidation;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitectureTemplate.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        var applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(applicationAssembly);
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));     // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));   
        });
        
        // Add Mapster 
        services.AddMapster();
        
        // đăng ký FluentValidation 
        services.AddValidatorsFromAssembly(applicationAssembly);
            //.AddFluentValidationAutoValidation();  add cái này là cho nó Auto return Error response 
            
        // đăng ký UserContext
        services.AddScoped<IUserContext, UserContext>();
        
        // đăng ký để sử dụng truy cập HttpContext 
        services.AddHttpContextAccessor();
    }
}