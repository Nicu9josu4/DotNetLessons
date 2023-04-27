using UseMVCProject.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddTransient<IGetTimerService, GetTimerService>();
        builder.Services.AddControllersWithViews(); // Adaugarea controllerilor cu view
        //builder.Services.AddMvc(options =>
        //{
        //    options.Filters.Add(typeof(GlobalSimpleFilter)); // Adaugarea filtrului la nivel de aplicatie
        //});
        //builder.Services.AddScoped<ActionSimpleFilter>(); // Pentru utilizarea atributului ServiceFilter
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.UseHttpsRedirection();
        app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}"
            );
        //app.MapDefaultControllerRoute(); // Sau de folosit o metoda imbricata in sistema care deja contine rutarea de mai sus

        app.MapGet("/", (IEnumerable<EndpointDataSource> endpointSources, HttpContext context) =>
        {
            var paths = string.Join("\n", endpointSources.SelectMany(source => source.Endpoints));
            context.Response.WriteAsync(paths);
        });

        app.Run();
    }
}