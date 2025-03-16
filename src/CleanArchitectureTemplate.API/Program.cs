using CleanArchitectureTemplate.API.Extensions;
using CleanArchitectureTemplate.API.Middlewares;
using CleanArchitectureTemplate.Application.Extensions;
using CleanArchitectureTemplate.Domain.Entities;
using CleanArchitectureTemplate.Infrastructure.Extensions;
using CleanArchitectureTemplate.Infrastructure.Seeders;

var builder = WebApplication.CreateBuilder(args);


builder.AddPresentation();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);


var app = builder.Build();

// thực hiện tạo Data từ Seeder mỗi khi start 
var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();
await seeder.Seed();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();                            // ExceptionHandler 
app.UseMiddleware<RequestTimeLoggingMiddleware>();    // Middlerware 

/*app.UseSerilogRequestLogging();*/

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGroup("api/identity")
        .WithTags("Identity")
        .MapIdentityApi<ApplicationUser>();

app.UseAuthorization();

app.MapControllers();

app.Run();


public partial class Program { }