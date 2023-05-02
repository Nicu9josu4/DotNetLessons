using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Text;
using TvShop.DatabaseService.HealthChecks;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
//builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
builder.Services.AddHealthChecksUI(setup => setup.SetEvaluationTimeInSeconds(int.Parse(builder.Configuration["HealthChecksUI:EvaluationTimeInSeconds"])))
    .AddInMemoryStorage();
//builder.Services.Configure<HealthChecksUIOptions>(options => options.);
//builder.Services.AddTransient<DatabaseHealthCheck>();
builder.Services.AddHealthChecks()
    .AddCheck<DatabaseHealthCheck>(nameof(DatabaseHealthCheck))
    .AddCheck<ConsistencyHealthCheck>(nameof(ConsistencyHealthCheck));
builder.Services.Configure<HealthCheckServiceOptions>(options =>
{
    options.Registrations.Select(content => content.Timeout = TimeSpan.FromSeconds(2));
});
// Add services to the container.
var services = builder.Services;

var app = builder.Build();
app.Environment.EnvironmentName = "Production";
//var serviceProvider = app.Services.GetServices<IServiceCollection>();
// Configure the HTTP request pipeline.
app.Map("/", async (HttpContext context) =>
{
    //var serviceProvider = app.Services.GetServices<IServiceCollection>();
    var sb = new StringBuilder();
    sb.Append("<h1>All Services</h1>");
    sb.Append("<table><thead>");
    sb.Append("<tr><th>Type</th><th>Lifetime</th><th>Instance</th></tr>");
    sb.Append("</thead><tbody>");
    foreach (var svc in services)
    {
        sb.Append("<tr>");
        sb.Append($"<td>{svc.ServiceType.FullName}</td>");
        sb.Append($"<td>{svc.Lifetime}</td>");
        sb.Append($"<td>{svc.ImplementationType?.FullName}</td>");
        sb.Append("</tr>");
    }
    sb.Append("</tbody></table>");
    await context.Response.WriteAsync(sb.ToString());
});
app.MapHealthChecks("/health", new HealthCheckOptions()
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
});

app.MapGet("healthchecks-ui/stop", (HttpContext context) =>
{
    //var service = app.Services.GetService<HealthCheckRegistration>();
    //service.Timeout = TimeSpan.FromSeconds(10);

    //app.Configuration["HealthChecksUI:EvaluationTimeInSeconds"] = "1000";
    context.Response.WriteAsync(app.Configuration["HealthChecksUI:EvaluationTimeInSeconds"]);
    //config["HealthChecksUI:EvaluationTimeInSeconds"] = "1000";
    //var myService = serviceProvider.GetRequiredService<IHealthCheck>();
    //var serviceCollection = serviceProvider.GetService<IServiceCollection>();
    //var descriptor = serviceCollection?.FirstOrDefault(service => service.ServiceType == typeof(IHealthCheck));

    //if(descriptor != null)
    //{
    //    serviceCollection?.Remove(descriptor);
    //}
    //databaseHealth.StopMonitoring();
    //var services = builder.Services;
    //var serviceToRemove = services.FirstOrDefault(service => service.ServiceType == typeof(IHealthCheck));
    //services.Remove(serviceToRemove);

    //var descriptor = builder.Services.FirstOrDefault(service => service.ServiceType == typeof(IHealthCheck));
    //builder.Services.Remove(descriptor);
    //builder.Services.Remove<IHealthCheck>();
});
//});
app.MapHealthChecksUI();
app.Run();