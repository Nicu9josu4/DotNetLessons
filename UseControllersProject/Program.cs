internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        var app = builder.Build();


        // Configure the HTTP request pipeline.

        app.UseHttpsRedirection();
        app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home1}/{action=Index}"
            );
        app.MapControllerRoute(
                name: "file",
                pattern: "{controller=GetFile}/{action=Index}"
            );

        app.Run();
    }
}