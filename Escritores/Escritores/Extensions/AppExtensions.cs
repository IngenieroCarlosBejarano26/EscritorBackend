namespace Escritores.Extensions;

public static class AppExtensions
{
    public static WebApplication UseProjectPipeline(this WebApplication app)
    {
        app.UseMiddleware<ExceptionMiddleware>();

        app.UseCors("AllowAll");

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        return app;
    }

    public static WebApplication ApplyMigrations(this WebApplication app)
    {
        using IServiceScope scope = app.Services.CreateScope();
        EscritoresDbContext db = scope.ServiceProvider.GetRequiredService<EscritoresDbContext>();
        db.Database.Migrate();

        return app;
    }
}
