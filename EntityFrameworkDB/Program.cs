using EntityFrameworkDB;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var connectionString = builder.Configuration.GetConnectionString("SqlConnection");
        builder.Services.AddTransient<MySqlConnection>(_ => new MySqlConnection(builder.Configuration.GetConnectionString("SqlConnection")));
        builder.Services.AddDbContext<ApplicationContext>(options =>
            options.UseMySql(ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("SqlConnection")))
        );
        // connectionString: builder.Configuration.GetConnectionString("SqlConnection"),


        //builder.Services.AddDbContext<ApplicationContext>();
        //builder.Services.AddControllers();
        //builder.Services.AddMvc();
        // Add services to the container.

        var app = builder.Build();
        app.UseRouting();
        // Configure the HTTP request pipeline.

        //app.MapGet("/", async context =>
        //{
        //    var dbContext = context.RequestServices.GetService<ApplicationContext>();
        //    var vacancies = await dbContext?.Vacancies.ToListAsync();
        //    await context.Response.WriteAsJsonAsync(vacancies);
        //});

        app.MapGet("/", (ApplicationContext db) => db.Vacancies.ToList());

        app.Run();
    }
}