using Microsoft.OpenApi.Models;
using Restaurants.API.Extentions;
using Restaurants.API.Middlewares;
using Restaurants.Domin.Entities;
using Restaurants.Infrastructure.Extensions;
using Restaurants.Infrastructure.Seeders;
using Serilog;
using Serilog.Events;
namespace Restaurants.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.AddPresentation();
            builder.Services.AddInfrastructure(builder.Configuration); // Extentions
            builder.Services.AddApplication();
            builder.Services.AddHttpContextAccessor(); // For IHttpContextAccessor

            // Add services to the container.

            var app = builder.Build();

            /////////////////////////////////
            var scope = app.Services.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();
            await seeder.Seed();
            //////////////////////////////////
            
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseMiddleware<RequestTimeLoggingMiddleware>();

            app.UseSerilogRequestLogging(); //Display the request
          

            // Configure the HTTP request pipeline.
            app.UseHsts();
            app.UseHttpsRedirection();

            
            app.MapGroup("api/identity")
                .WithTags("Identity") // to map identity controler togather
                .MapIdentityApi<User>(); // To appear in swagger

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    } 
}
public partial class Program { }
// It's aloow us to use program class outside prsentation layer 
// we do it to can access the program in integration testing