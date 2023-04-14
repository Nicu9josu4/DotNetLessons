using EntityFrameworkDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddHealthChecks();

        //builder.Services.AddDbContext<ApplicationContext>();
        //builder.Services.AddControllers();
        //builder.Services.AddMvc();
        // Add services to the container.

        var app = builder.Build();
        app.UseRouting();
        // Configure the HTTP request pipeline.

        app.MapHealthChecks("/health"); // Verificarea sanatatii programului


        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGet("/", async context =>
            {
                var dbContext = context.RequestServices.GetService<ApplicationContext>();
                var vacancies = await dbContext?.Vacancies.ToListAsync();
                await context.Response.WriteAsJsonAsync(vacancies);
            });
        });

        //app.MapGet("/", (ApplicationContext db) => db.Vacancies.ToList());

        app.Run();
    }
}