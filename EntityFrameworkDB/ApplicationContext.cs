using EntityFrameworkDB.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkDB
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Vacancy> VACANCIES { get; set; } // Colectia din OracleDB
        public DbSet<Users> USERS { get; set; } // Colectia din Oracle
        public DbSet<User> users { get; set; } // Colectia din PostgreSQL si mySql
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            //Database.EnsureCreated();
            Database.Migrate();
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<SchoolDBContext, EF6Console.Migrations.Configuration>());
            //Database.MigrateAsync();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseOracle("Data Source=localhost:1521/orcl.moldcell.intern;Persist Security Info=True;User ID=WebDeveloper;Password=Nicu9josu4");
            //optionsBuilder.UseMySQL("Server=localhost;Port=3306;Uid=root;Pwd=root;Database=world");
            //optionsBuilder.UseNpgsql("Host=127.0.0.1:5432;Database=world;Username=postgres;Password=root");

        }

        // Pentru Baza de date in cazul in care unele coloane nu au chee primara
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<User>()
            //    .HasNoKey();
            modelBuilder.Entity<Vacancy>()
                .HasNoKey();
        }
    }
}