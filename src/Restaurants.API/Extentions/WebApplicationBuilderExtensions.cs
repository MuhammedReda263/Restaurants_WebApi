using Microsoft.OpenApi.Models;
using Restaurants.API.Middlewares;
using Restaurants.Domin.Entities;
using Serilog;

namespace Restaurants.API.Extentions
{
    public static class WebApplicationBuilderExtensions
    {
        public static void AddPresentation(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(); // Enable authorize work
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();

            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearerAuth"}
            },
            []
        }

    });
                // DateOnly Schema Filter
                c.SchemaFilter<DateOnlySchemaFilter>();
            });





            builder.Host.UseSerilog((context, configuration) =>
                 configuration.ReadFrom.Configuration(context.Configuration)
            );


            builder.Services.AddScoped<ErrorHandlingMiddleware>();
            builder.Services.AddScoped<RequestTimeLoggingMiddleware>();
        }
    }
}
