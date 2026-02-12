using Escritores.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddProjectServices(builder.Configuration)
    .AddCorsConfiguration()
    .AddJwtAuth(builder.Configuration)
    .AddCustomSwagger();

WebApplication app = builder.Build();

app.ApplyMigrations();
app.UseProjectPipeline();

app.Run();
