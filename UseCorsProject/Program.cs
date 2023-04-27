internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        //builder.Services.AddCors();
        builder.Services.AddCors(options => options.AddPolicy("TestPolicy", builder => builder
            .WithOrigins("https://localhost:7023")
            .AllowAnyHeader()
            .AllowAnyMethod()
        ));
        var app = builder.Build();

        // Configure the HTTP request pipeline.

        app.UseHttpsRedirection();

        app.UseCors(builder => builder.AllowAnyOrigin());

        app.Map("/", async context => await context.Response.WriteAsync("Hello from corsed project"));

        app.Map("/cors", () => "Ai accesat datele din Cors").RequireCors("TestPolicy");

        app.Run();
    }
}