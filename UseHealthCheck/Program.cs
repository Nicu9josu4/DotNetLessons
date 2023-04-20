using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.DependencyInjection;
using TvShop.DatabaseService.HealthChecks;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers();
        builder.Services.AddHealthChecksUI().AddInMemoryStorage();
        builder.Services.AddHealthChecks()
            .AddCheck<DatabaseHealthCheck>(nameof(DatabaseHealthCheck))
            .AddCheck<ConsistencyHealthCheck>(nameof(ConsistencyHealthCheck));
        // Add services to the container.

        var app = builder.Build();

        // Configure the HTTP request pipeline.

        app.UseHttpsRedirection();

        app.UseRouting();

        app.MapControllers();
        app.MapHealthChecks("/health", new HealthCheckOptions()
        {
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });

        app.Run();
    }
}