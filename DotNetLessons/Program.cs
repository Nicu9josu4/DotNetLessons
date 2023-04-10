using DotNetLessons.FileLogger;
using DotNetLessons.Services;
using DotNetLessons.Services.MapRoutes;
using Microsoft.AspNetCore.Mvc;


namespace DotNetLessons
{
    public class Program
    {
        /*                     Lifecycle: (Ciclul de viata a serviciilor)
                          |-------------------------------------------|  |------------------------|
             Singletone:  |-----------------------------------------------------------------------|
             Scoped:      |-------------------------------------------|  |------------------------|
             Transient:   |------------|  |--|               |--------|  |--------|  |------------|
        */

        public static void Main(string[] args)
        {
            #region Build region

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            // Add services to the container.
            builder.Services.AddTransient<IGreeter, SimpleGreeter>();
            builder.Services.AddTransient<IGreeter, GrandGreeter>();

            /// Adding a Session services:
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession();
            builder.Services.AddControllers();
            /// Adaugarea unei restrictii pentru utilizarea Map
            builder.Services.Configure<RouteOptions>(options =>
                options.ConstraintMap.Add("secretcode", typeof(SecretCodeConstraint)));
            //builder.Services.AddTransient<GreeterWithConstructorService>();
            //builder.Services.AddTransient<GreeterWithPropertyService>();
            //builder.Services.AddTransient<GreeterWithMethodService>();
            /// Exception handling
            WorkWithExceptions.BuildException(builder);

            /// Configuration
            WorkWithConfigs.BuildConfig(builder);

            /// Loggining
            WorkWithLoggers.BuildLogg(builder);

            /// Authorisation and Authentification
            WorkWithAuthorisationAndAuthentificaion.BuildAuthorisationAndAuthentification(builder);
            #endregion

            var app = builder.Build();

            #region Call Services

            var greetService = app.Services.GetService<IGreeter>();

            //foreach (var greetService in greetServices)
            //{
            GreeterWithConstructorService greeterServiceCtor = new(greetService);
            greeterServiceCtor.GreetUser("Tom");

            GreeterWithPropertyService greeterServiceProp = new()
            {
                Greeter = greetService
            };
            greeterServiceProp.GreetUser("Bob");

            GreeterWithMethodService greeterServiceMethod = new();
            greeterServiceMethod.GreetUser(greetService, "Miron");
            //}

            #endregion Call Services

            //Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }



            WorkWithAuthorisationAndAuthentificaion.ApplicationAuthentication(app); // Authentication
            WorkWithAuthorisationAndAuthentificaion.ApplicationAuthorization(app);  // Authorization
            WorkWithConfigs.ApplicationConfig(app);                                 // Configuration
            WorkWithLoggers.ApplicationLogg(app);                                   // Loggining
            WorkWithExceptions.ApplicationException(app);                           // Exception
            WorkWithResultsAPI.ApplicationResult(app);                              // Results
            WorkWithWebAPI.ApplicationWeb(app);                                     // WebAPI 
            /// Using middlewares
            app.UseSession(); // Use Session middleware
            app.UseHttpsRedirection();

            /// Using a static files
            //app.UseFileServer();
            //app.UseDefaultFiles();
            app.UseStaticFiles();

            /// Using system routing
            //app.UseRouting();
            //app.MapControllers();
            //app.Map("/", async (context) =>
            //{
            //    context.Response.ContentType = "text/html";
            //    await context.Response.SendFileAsync(@"wwwroot/index.html");

            //});


            //app.Use(async (context, next) =>
            //{
            //    //Console.WriteLine("Custom anonym middleware is starting!");
            //    context.Items.Add("Message", "Hello to all");

            //    await next.Invoke(context);
            //    //Console.WriteLine("Custom anonym middleware is stoped!");
            //});

            app.MapGet("/a", async (context) =>
            {
                if (context.Request.Cookies.ContainsKey("Name"))
                {
                    var name = context.Request.Cookies["Name"];
                    await context.Response.WriteAsync(" Hello from index page Mister " + name);
                    Console.WriteLine($"Hello from index page Mister {name}");
                }
                else
                {
                    context.Response.Cookies.Append("Name", "Cookie");
                    await context.Response.WriteAsync(" Hello from index");
                }
            });

            //app.Use(async (context, next) =>
            //{
            //    //Console.WriteLine("Custom anonym middleware 2 is starting!");
            //    context.Items.Add("Message2", "Hello to all peoples");
            //    await next.Invoke(context);
            //    //Console.WriteLine("Custom anonym middleware 2 is stoped!");
            //});

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //    endpoints.MapGet("/112", async context => await context.Response.WriteAsync("Hello, World!"));
            //});

            /// Use routeRestriction
            app.Map("/house/{code:secretcode(admin)}", (string code) => $"Your code is: '{code}'. Welcome to house");
            /// Probe ale unor errori bazate pe Map
            app.Map("/greet1", (SimpleGreeter greeter) => $"{greeter.Greet("Greeter 1")} Welcome"); // Broken
            app.Map("/greet2", () => $"{greetService.Greet("Greeter 2")} Welcome"); // Work
            app.Map("/greet3", (IGreeter greeter) => $"{greeter.Greet("Greeter 3")} Welcome"); // Work
            app.Map("/greet4", (User user) => $"{user.PrintUser()} Greeter 4 Welcome"); // Broken
            app.Map("/greet5", () => new User().PrintUser() + " Greeter 5"); // Work
            app.Map("/greet6", () => User.PrintStaticUser() + " Greeter 6"); // Work

            

            /// Use session method
            app.MapGet("/", (IEnumerable<EndpointDataSource> endpointSources, HttpContext context) =>
            {
                if (context.Session.Keys.Contains("name"))
                {
                    context.Response.WriteAsync($"Hello from session {context.Session.GetString("name")}!\n");
                }
                else
                {
                    context.Session.SetString("name", "IoJo");
                }

                var paths = string.Join("\n", endpointSources.SelectMany(source => source.Endpoints));
                context.Response.WriteAsync(paths);
            });

            app.Run();
        }
    }

    public class User
    {
        public string Name { get; set; } = "Undefined";
        public string Password { get; set; } = "None";

        public string PrintUser() => $"dynamic Name: {Name} + dynamic Pasword:{Password}";

        public static string PrintStaticUser() => $"Satic Name + Static password";
        public void Logg(ILogger logger)
        {
            logger.LogInformation("Messaj de informare din FileLoger");
        }
    }
    public class LogClass
    {
        private readonly ILogger _logger;

        public LogClass(ILogger logger)
        {
            _logger = logger;
        }
        public void Log(string message)
        {
            _logger.LogInformation(message);
        }
        public void LogError(string message)
        {
            _logger.LogError(message);
        }
    }
    public class LogClass2
    {
        private readonly ILogger _logger;

        public LogClass2(ILoggerFactory factorylogger)
        {
            _logger = factorylogger.CreateLogger("Program");
        }
        public void Log(string message)
        {
            _logger.LogInformation(message);
        }
        public void LogError(string message)
        {
            _logger.LogError(message);
        }
    }

}