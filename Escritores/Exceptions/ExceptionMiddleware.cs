namespace Escritores.Exceptions;

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<ExceptionMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        (HttpStatusCode statusCode, string message) = exception switch
        {
            ValidationException validation => (HttpStatusCode.BadRequest, string.Join("; ", validation.Errors.Select(e => e.ErrorMessage))),
            AuthorNotFoundException authorEx => (HttpStatusCode.BadRequest, authorEx.Message),
            MaxBooksReachedException maxEx => (HttpStatusCode.BadRequest, maxEx.Message),
            DomainException domainEx => (HttpStatusCode.BadRequest, domainEx.Message),
            _ => (HttpStatusCode.InternalServerError, "Ocurrió un error interno.")
        };

        if ((int)statusCode >= 500)
            _logger.LogError(exception, "Error no controlado: {Message}", exception.Message);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        ProblemDetails problem = new()
        {
            Status = (int)statusCode,
            Title = statusCode == HttpStatusCode.BadRequest ? "Solicitud incorrecta" : "Error del servidor",
            Detail = message
        };

        string json = JsonSerializer.Serialize(problem, JsonOptions);
        await context.Response.WriteAsync(json);
    }

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
}

