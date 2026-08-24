// TODO: implement global exception handling middleware.
//
// Standard ASP.NET Core pattern: a middleware that wraps the rest of the pipeline in try/catch,
// logs the exception, and returns a consistent error response (e.g. ProblemDetails) instead of
// letting unhandled exceptions leak a stack trace to the client.
//
//   public class ExceptionMiddleware
//   {
//       private readonly RequestDelegate _next;
//       public ExceptionMiddleware(RequestDelegate next) => _next = next;
//
//       public async Task InvokeAsync(HttpContext context)
//       {
//           try { await _next(context); }
//           catch (Exception ex)
//           {
//               // log ex, then write a ProblemDetails response with an appropriate status code
//           }
//       }
//   }
//
// Register in program.cs with app.UseMiddleware<ExceptionMiddleware>() near the top of the
// pipeline (before routing), so it catches exceptions from everything downstream.
