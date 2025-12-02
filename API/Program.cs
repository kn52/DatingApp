using API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Register all services (Swagger, Controllers, EF, etc.)
builder.AddExtServices();

var app = builder.Build();

// Configure middleware pipeline (Swagger, Routing, etc.)
app.AddExtPipelines();
