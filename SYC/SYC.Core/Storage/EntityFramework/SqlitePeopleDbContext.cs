using Microsoft.EntityFrameworkCore;
using SYC.Core.Models;
using SYC.Core.Storage.EntityFramework.Configuration;
namespace SYC.Core.Storage.EntityFramework
{
    public class SqlitePeopleDbContext : DbContext
    {
        public DbSet<Person> People { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data source=peopleDb.db3");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PersonConfiguration());
        }
    }
}