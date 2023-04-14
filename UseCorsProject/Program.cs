internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddCors();
        var app = builder.Build();

        // Configure the HTTP request pipeline.

        app.UseHttpsRedirection();

        app.UseCors(builder => builder.AllowAnyOrigin());

        app.Map("/", async context => await context.Response.WriteAsync("Hello from corsed project"));


        app.Run();
    }
}