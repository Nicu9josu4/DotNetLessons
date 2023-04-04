﻿using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace DotNetLessons
{
    public class WorkWithConfigs
    {
        // Work with builder
        internal static void BuildConfig(WebApplicationBuilder? builder)
        {
            /// Using a IConfigurationRoot 
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("JsonConfigs/person.json", optional: true, reloadOnChange: true)
                .AddJsonFile("JsonConfigs/tom.json", optional: true, reloadOnChange: true)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true); // Permiterea adaugarii fisierelor in mod dinamic

            IConfiguration configuration = configurationBuilder.Build();
            var val = configuration.GetConnectionString("DefaultConnection");
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
            var conn = connectionSection.GetConnectionString("DefaultConnection");

        }
        // Work with Application
        internal static void ApplicationConfig()
        {

        }
    }


}
