using Microsoft.EntityFrameworkCore;
using SYC.Core.Models;
using SYC.Core.Storage.EntityFramework.Configuration;
namespace SYC.Core.Storage.EntityFramework
{
    public class SqlServerPeopleDbContext : DbContext
    {
        public DbSet<Person> People { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=PeopleDb;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PersonConfiguration());
        }
    }
}