namespace API.Middleware;

/// <summary>
/// Catches every unhandled exception and returns a flat 500 with a safe, generic message -- the
/// real exception (stack trace, SQL, etc.) is logged server-side only, never sent to the caller.
/// </summary>
/// <remarks>
/// TODO (later, not now): map specific exception types to specific status codes if/when useful,
/// e.g. a "not found" domain exception -> 404, a validation exception -> 400. For now everything
/// is a flat 500 by design.
/// </remarks>
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
