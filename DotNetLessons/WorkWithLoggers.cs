using DotNetLessons.FileLogger;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DotNetLessons
{
    public class WorkWithLoggers
    {
        // Work with builder
        internal static void BuildLogg(WebApplicationBuilder builder)
        {
            builder.Services.AddLogging();
            //builder.Logging.ClearProviders();
            builder.Logging.AddFile("Logger.txt");
            //builder.Services.AddHttpLogging(logg => { });
            //builder.Logging.AddFile(Path.Combine(Directory.GetCurrentDirectory(), "logger.txt"));

        }
        // Work with builder
        internal static void ApplicationLogg(WebApplication app)
        {
            app.Logger.LogInformation("Aici se logeaza o informatie ");

            /// Utilizarea interfetei ILogger cu categoria WorkWithLoggers
            app.Map("/log", (ILogger<WorkWithLoggers> logger) => // WorkWithLoggers fiind ca tip de date generic
            {                                                    // Reprezinta o categorie, ceea ce va afisa inainte de mesaj, adica Documentul de unde a fost chemata
                logger.LogInformation("Logarea informatiei");
                logger.LogWarning("Logarea unei exceptii minore");
                logger.LogError("Logarea unei errori");
                logger.LogCritical("Logare Criticala");
            });
            /// Utilizarea interfetei ILogger cu categoria Program
            app.Map("/log1", (ILogger<Program> logger) => // WorkWithLoggers fiind ca tip de date generic
            {                                                    // Reprezinta o categorie, ceea ce va afisa inainte de mesaj, adica Documentul de unde a fost chemata
                logger.LogInformation("Logarea informatiei");
                logger.LogWarning("Logarea unei exceptii minore");
                logger.LogError("Logarea unei errori");
                logger.LogCritical("Logare Criticala");
            });


            /// Nivelele de logare:
            /// Trace       - LogTrace()        : nivelul care transmite un mesaj cat mai detailat, este bine sa-l folosim atunci cand creem aplicatia dar nu atunci cand deja este gata
            /// Debug       - LogDebug()        : Pentru afisarea informatiei ce poate ajuta int timpul creearii aplicatiei
            /// Information - LogInformation()  : Nivelul care permite urmarirea executiei programului
            /// Warning     - LogWarning()      : Este folosit pentru afisarea unor exceptii care nu orpesc programul (Ne critice)
            /// Error       - LogError()        : Este folosit pentru afisarea exceptiilor si erorilor
            /// Critical    - LogCritical()     : Nivelul critic de eroare, atunci cand necesita evaluarea lor momentan
            /// None                            : Nivelul care nu necesita de a afisa ceva in loguri


            /// Utilizarea interfetei ILoggerFactory
            ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
                builder.AddConsole() // Locatia unde sa fie inregistrate logurile
                .AddDebug()
            );
            ILogger logger1 = loggerFactory.CreateLogger<Program>(); // Categoria Program
            ILogger logger2 = loggerFactory.CreateLogger("WebApplication"); // Categoria WebApplication

            app.Map("/logFactory", async (context) =>
            {
                logger1.LogWarning("Logarea unei exceptii din LoggFactory cu ajutorul categoriei Program");
                logger2.LogInformation("Logarea unei exceptii din LoggFactory  cu ajutorul categoriei WebApplication");
                await context.Response.WriteAsync($"{context.Request.Path}");
            });

            app.Map("/log2", (ILogger logger) =>
            {

            });

            app.Map("/greet7", (ILogger<Program> logger) => logger.LogInformation("Heello")); // Work
            app.Map("/greet8", (ILogger<Program> logger) => logger.LogError("Logarea unei Erori")); // Work
            /// Utilizarea Middleware-ului UseHttpLoggining
            app.UseHttpLogging(); /// -> Vezi in appsettings.Development.json ????????
        }
    }
}
