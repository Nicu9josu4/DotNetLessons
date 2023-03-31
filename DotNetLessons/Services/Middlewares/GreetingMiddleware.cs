namespace DotNetLessons.Middlewares
{
    public class GreetingMiddleware
    {
        private readonly RequestDelegate _next;

        public GreetingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path == "/greetMid")
            {
                context.Response.Headers.Add("Greeting", "Hello to all from custom middleware");
                await context.Response.WriteAsync("Hello Ion");
            }
            else
            {
                await _next.Invoke(context);
            }
        }
    }

    public static class GreetingMiddlewareExtensions
    {
        public static IApplicationBuilder UseGreetingMiddleware(this IApplicationBuilder builder) =>
            builder.UseMiddleware<GreetingMiddleware>();
    }
}