// TODO: implement request logging middleware.
//
// Standard ASP.NET Core pattern: a middleware that logs method/path/status code/duration for each
// request.
//
//   public class LoggingMiddleware
//   {
//       private readonly RequestDelegate _next;
//       private readonly ILogger<LoggingMiddleware> _logger;
//
//       public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
//       {
//           _next = next;
//           _logger = logger;
//       }
//
//       public async Task InvokeAsync(HttpContext context)
//       {
//           var stopwatch = Stopwatch.StartNew();
//           await _next(context);
//           _logger.LogInformation("{Method} {Path} -> {StatusCode} in {Elapsed}ms",
//               context.Request.Method, context.Request.Path, context.Response.StatusCode,
//               stopwatch.ElapsedMilliseconds);
//       }
//   }
//
// Register in program.cs with app.UseMiddleware<LoggingMiddleware>(). If you want persisted
// audit-trail-style logging (not just console/ILogger output), that's what
// Application.ActivityLogs.CreateActivityLog is for -- call it from wherever an action worth
// auditing happens, not from this middleware.
