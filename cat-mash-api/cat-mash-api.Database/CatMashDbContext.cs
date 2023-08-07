using cat_mash_api.Database.Helpers.Seed;
using cat_mash_api.Database.Shared.EntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Reflection;
using System.Text.Json;

namespace cat_mash_api.Database
{
    public class CatMashDbContext : DbContext
    {
        public DbSet<Cat> Cats { get; set; }

        public DbSet<Vote> Votes { get; set; }

        public CatMashDbContext(DbContextOptions<CatMashDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            CatsJson json = new CatsJson();
            List<Cat> cats = JsonSerializer.Deserialize<List<Cat>>(json.catJsonString);
            modelBuilder.Entity<Cat>().HasData(cats);
        }
    }

    public class UserDbContextFactory : IDesignTimeDbContextFactory<CatMashDbContext>
    {
        public CatMashDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CatMashDbContext>();
            var connectionString = "Server=localhost;Port=3306;User=root;password=root;database=CatMashAPI-DB;";
            optionsBuilder.UseMySql(connectionString, MySqlServerVersion.AutoDetect(connectionString));

            return new CatMashDbContext(optionsBuilder.Options);
        }
    }
}
