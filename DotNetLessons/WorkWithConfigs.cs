namespace DotNetLessons
{
    public class WorkWithConfigs
    {
        // Work with builder
        internal static void BuildConfig(WebApplicationBuilder builder)
        {
            /// Using a IConfigurationRoot
            var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile("JsonConfigs/person.json", optional: true, reloadOnChange: true)
                .AddJsonFile("JsonConfigs/tom.json", optional: true, reloadOnChange: true)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true); // Permiterea adaugarii fisierelor in mod dinamic

            IConfiguration configuration = configurationBuilder.Build();
            var val = configuration.GetConnectionString("DefaultConnection");
            var val1 = configuration.GetSection("Connections")["Con"];
            /// Sau
            IConfigurationRoot configurationRoot = configurationBuilder.Build();
            var value = configurationRoot.GetConnectionString("DefaultConnection");

            builder.Configuration
                .SetBasePath(Directory.GetCurrentDirectory()); /// Indicarea locatiei de unde va putea accesa fisierele de configurare
            builder.Configuration.AddConfiguration(configurationBuilder.Build());

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            ///Folosirea IConfigurationSection in cazul in care avem mai multe conexiuni in fisierul configuratiilor
            ///Aceasta metoda deja este implementata in interfata IConfiguration si poate fi chemat precum modul de mai sus
            IConfigurationSection? connectionSection = builder.Configuration.GetSection("ConnectionStrings");
            var conn = connectionSection.GetConnectionString("DefaultConnection:");
            /// sau
            var connn = connectionSection["DefaultConnection"];
        }

        // Work with Application
        internal static void ApplicationConfig(WebApplication app)
        {
            /// Adding dynamic configuration
            app.Configuration["Place"] = "Moldcell";
            app.Configuration["TimeNow"] = DateTime.Now.ToString();

            app.Map("/anonimConfig", (IConfiguration config) => ($"Place: {config["Place"]} - Time: {config["TimeNow"]}"));
            //app.Map("/sqlConfig", (IConfiguration config));

            /// Access peoples from json files
            app.Map("/tom", (IConfiguration config) => ($"Name: {config["Name"]} - Age:{config["Age"]} - WorkPlace:{config["WorkPlace"]}"));
            app.Map("/person", (IConfiguration config) => $"Name: {config["person:profile:name"]} - Company: {config["company:name"]}");
        }
    }
}