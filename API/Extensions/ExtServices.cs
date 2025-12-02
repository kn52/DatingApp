using System;
using API.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class ExtServices
{
    public static IServiceCollection AddExtServices(this WebApplicationBuilder webApplicationBuilder)
    {
        IServiceCollection services = webApplicationBuilder.Services;
        ConfigurationManager configuration = webApplicationBuilder.Configuration;

        services.AddDbContext<AppDBContext>(opt =>
        {
           opt.UseSqlite(configuration["ConnectionStrings:DefaultConnection"]); 
        });


        return services;
    }

    public static WebApplication AddExtPipelines(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();

        return app;
    }
}
