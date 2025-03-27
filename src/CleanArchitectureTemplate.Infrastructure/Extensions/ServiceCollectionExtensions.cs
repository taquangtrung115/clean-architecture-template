using CleanArchitectureTemplate.Domain.Entities;
using CleanArchitectureTemplate.Domain.Interfaces;
using CleanArchitectureTemplate.Domain.Repositories;
using CleanArchitectureTemplate.Infrastructure.Persistence;
using CleanArchitectureTemplate.Infrastructure.Repositories;
using CleanArchitectureTemplate.Infrastructure.Seeders;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitectureTemplate.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Add Cors
        services.AddCors(o => o.AddPolicy("AllowAllOrigins", builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        }));
        services.ConfigureApplicationCookie(o =>
        {
            o.Events = new CookieAuthenticationEvents()
            {
                OnRedirectToLogin = (ctx) =>
                {
                    if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
                    {
                        ctx.Response.StatusCode = 401;
                    }

                    return Task.CompletedTask;
                },
                OnRedirectToAccessDenied = (ctx) =>
                {
                    if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
                    {
                        ctx.Response.StatusCode = 403;
                    }

                    return Task.CompletedTask;
                }
            };
        });
        var connectionString = configuration.GetConnectionString("DB");
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

        //services.AddIdentityApiEndpoints<ApplicationUser>()
        //    .AddRoles<IdentityRole>()
        //    //.AddClaimsPrincipalFactory<RestaurantsUserClaimsPrincipalFactory>()
        //    .AddEntityFrameworkStores<ApplicationDbContext>()
        //    .AddApiEndpoints();
        
        // đăng kí seeder 
        services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
        
        // Đăng kí repo (sử dụng trong trường hơpj cần inject trực tiếp vào để xử lý trong Handler)
        services.AddScoped<IProfileRepository, ProfileRepository>();
        services.AddScoped<IAdditionImgUrlReponsitory, AdditionImgUrlReponsitory>();
        services.AddScoped<IAddressReponsitory, AddressReponsitory>();
        services.AddScoped<IBrandReponsitory, BrandReponsitory>();
        services.AddScoped<ICategoryReponsitory, CategoryReponsitory>();
        services.AddScoped<IOrderDetailReponsitory, OrderDetailReponsitory>();
        services.AddScoped<IOrderReponsitory, OrderReponsitory>();
        services.AddScoped<IProductReponsitory, ProductReponsitory>();
        services.AddScoped<IReviewReponsitory, ReviewReponsitory>();
        
        // Unit Of Work 
        services.AddScoped<IUnitOfWork, UnitOfWork>();

       
    }
}
