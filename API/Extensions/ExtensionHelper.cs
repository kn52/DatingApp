using API.Data;
using API.DbServices.BaseLayer;
using API.DbServices.Members;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace API.Extensions;


// ==== Extraneous ====
public static class ExtensionHelper { }



//==== Services ====
public static class ExtServices
{
    public static IServiceCollection AddExtServices(this WebApplicationBuilder builder)
    {
        IServiceCollection services = builder.Services;
        ConfigurationManager configuration = builder.Configuration;

        services.AddControllers();

        // Swagger for .NET 8
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "DatingApp API",
                Version = "v1",
                Description = "DatingApp API documentation"
            });
        });

        // EF + SQLite
        services.AddDbContext<AppDBContext>(opt =>
        {
            opt.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
        });


        services.AddScoped(typeof(Repository<,>));
        services.AddScoped<MembersRepository>();
        services.AddScoped<MembersService>();


        return services;
    }
}



//==== Pipelines ====
public static class ExtPipelines
{
    public static void AddExtPipelines(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "DatingApp API v1");
                c.RoutePrefix = "swagger"; // Swagger at https://localhost:5001/swagger
            });
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
