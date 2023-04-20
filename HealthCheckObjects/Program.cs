internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddHealthChecksUI().AddInMemoryStorage();
        // Add services to the container.

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.MapHealthChecksUI();
        app.Run();
    }
}