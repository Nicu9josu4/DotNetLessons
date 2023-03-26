using DotNetLessons.Middlewares;
using DotNetLessons.Services;
using DotNetLessons.Services.MapRoutes;
using Microsoft.Extensions.DependencyInjection;

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

            //builder.Services.AddTransient<GreeterWithConstructorService>();
            //builder.Services.AddTransient<GreeterWithPropertyService>();
            //builder.Services.AddTransient<GreeterWithMethodService>();

            /// Configuration
            builder.Configuration
                .AddJsonFile("JsonConfigs/person.json")
                .AddJsonFile("JsonConfigs/tom.json");

            var app = builder.Build();

            #region Call Services
            var greetServices = app.Services.GetServices<IGreeter>();

            foreach (var greetService in greetServices)
            {
                GreeterWithConstructorService greeterServiceCtor = new(greetService);
                greeterServiceCtor.GreetUser("Tom");

                GreeterWithPropertyService greeterServiceProp = new()
                {
                    Greeter = greetService
                };
                greeterServiceProp.GreetUser("Bob");

                GreeterWithMethodService greeterServiceMethod = new();
                greeterServiceMethod.GreetUser(greetService, "Miron");
            }

            #endregion Call Services
            //Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            /// Using exception handling
            app.UseDeveloperExceptionPage();

            /// Using middlewares
            app.UseGreetingMiddleware();
            app.UseHttpLogging();

            /// Using a static files
            //app.UseFileServer();

            /// Using system routing
            app.MapControllers();
            app.Map("/", async (context) =>
            {
                context.Response.ContentType = "text/html";
                await context.Response.SendFileAsync(@"wwwroot/index.html");

            });
            app.Use(async (context, next) =>
            {
                Console.WriteLine("Custom anonym middleware is starting!");
                await next.Invoke(context);
                Console.WriteLine("Custom anonym middleware is stoped!");
            });

            app.Use(async (context, next) =>
            {
                Console.WriteLine("Custom anonym middleware 2 is starting!");
                await next.Invoke(context);
                Console.WriteLine("Custom anonym middleware 2 is stoped!");
            });
            app.Configuration["Place"] = "Moldcell";
            app.Configuration["TimeNow"] = DateTime.Now.ToString();
            app.Map("/anonimConfig", (IConfiguration config) => ($"Place: {config["Place"]} - Time: {config["TimeNow"]}"));

            ///Access peoples from json files
            app.Map("/tom", (IConfiguration config) => ($"Name: {config["Name"]} - Age:{config["Age"]} - WorkPlace:{config["WorkPlace"]}"));
            app.Map("/person", (IConfiguration config) => ($"Name: {config["person:profile:name"]} - Company: {config["company:name"]}"));

            app.Run();
        }
    }
}