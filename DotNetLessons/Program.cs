using DotNetLessons.Services;
using DotNetLessons.Services.MapRoutes;

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

            /// Adaugarea unei restrictii pentru utilizarea Map
            builder.Services.Configure<RouteOptions>(options =>
                options.ConstraintMap.Add("secretcode", typeof(SecretCodeConstraint)));
            //builder.Services.AddTransient<GreeterWithConstructorService>();
            //builder.Services.AddTransient<GreeterWithPropertyService>();
            //builder.Services.AddTransient<GreeterWithMethodService>();

            /// Configuration
            builder.Configuration
                .AddJsonFile("JsonConfigs/person.json")
                .AddJsonFile("JsonConfigs/tom.json");

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

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.Environment.EnvironmentName = "Production";
            /// Using exception handling
            app.UseDeveloperExceptionPage(); // Catch an exception when Environment is Development
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler(app => app.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsync("Error 500. DivideByZeroException occured!");
                }));
            }

            /// Using middlewares
            //app.UseGreetingMiddleware();
            app.UseHttpLogging();
            app.UseSession(); // Use Session middleware

            /// Using a static files
            //app.UseFileServer();
            //app.UseDefaultFiles();
            app.UseStaticFiles();

            /// Using system routing
            app.UseRouting();
            //app.MapControllers();
            //app.Map("/", async (context) =>
            //{
            //    context.Response.ContentType = "text/html";
            //    await context.Response.SendFileAsync(@"wwwroot/index.html");

            //});

            /// Using a ResultsAPI
            Results.Ok();

            app.Use(async (context, next) =>
            {
                Console.WriteLine("Custom anonym middleware is starting!");
                context.Items.Add("Message", "Hello to all");

                await next.Invoke(context);
                Console.WriteLine("Custom anonym middleware is stoped!");
            });

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

            app.Use(async (context, next) =>
            {
                Console.WriteLine("Custom anonym middleware 2 is starting!");
                context.Items.Add("Message2", "Hello to all peoples");
                await next.Invoke(context);
                Console.WriteLine("Custom anonym middleware 2 is stoped!");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/112", async context => await context.Response.WriteAsync("Hello, World!"));
            });

            /// Adding dynamic configuration
            app.Configuration["Place"] = "Moldcell";
            app.Configuration["TimeNow"] = DateTime.Now.ToString();

            app.Map("/anonimConfig", (IConfiguration config) => ($"Place: {config["Place"]} - Time: {config["TimeNow"]}"));

            /// Access peoples from json files
            app.Map("/tom", (IConfiguration config) => ($"Name: {config["Name"]} - Age:{config["Age"]} - WorkPlace:{config["WorkPlace"]}"));
            app.Map("/person", (IConfiguration config) => $"Name: {config["person:profile:name"]} - Company: {config["company:name"]}");

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
            /// Use Exception handler methods
            app.MapGet("/getException", async context =>
            {
                int a = 10;
                int b = 0;
                await context.Response.WriteAsync($"{a / b}");
            });

            //app.Run(async context =>
            //{
            //    int a = 10;
            //    int b = 0;
            //    await context.Response.WriteAsync($"{a / b}");
            //});


            app.Run(async context =>
                await context.Response.WriteAsync("Go to home writing '/'")
            );

            app.Run();
        }
    }

    public class User
    {
        public string Name { get; set; } = "Undefined";
        public string Password { get; set; } = "None";

        public string PrintUser() => $"dynamic Name: {Name} + dynamic Pasword:{Password}";

        public static string PrintStaticUser() => $"Satic Name + Static password";
    }
}