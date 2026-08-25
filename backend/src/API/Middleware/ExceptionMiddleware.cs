namespace API.Middleware;

// Catches every unhandled exception and returns a generic 500 with a safe message -- never the
// exception details, so nothing internal (stack traces, SQL, connection strings) leaks to callers.
// TODO (later, not now): map specific exception types to specific status codes if/when useful,
// e.g. a "not found" domain exception -> 404, a validation exception -> 400. For now everything
// is a flat 500 by design.
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception while processing {Method} {Path}",
                context.Request.Method, context.Request.Path);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            await context.Response.WriteAsJsonAsync(new { error = "An unexpected error occurred." });
        }
    }
}
