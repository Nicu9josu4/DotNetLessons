using EntityFrameworkDB;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<DatabaseContext>(options => options.UseOracle(connectionString));
        // Add services to the container.

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.MapGet("/", (DatabaseContext db) => db.Vacancies.ToList());

        app.Run();
    }
}