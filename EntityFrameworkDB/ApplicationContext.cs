using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkDB
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Vacancy> Vacancies { get; set; } // Colectie de obiecte care reprezinta aceeasi tabela din baza de date
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }
    }
}
