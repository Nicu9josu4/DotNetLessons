using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkDB
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Vacancy> Vacancies { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseOracle("Data Source=localhost:1521/orcl.moldcell.intern;Persist Security Info=True;User ID=WebDeveloper;Password=Nicu9josu4");
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
