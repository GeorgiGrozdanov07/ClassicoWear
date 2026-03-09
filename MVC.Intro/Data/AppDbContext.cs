using Microsoft.EntityFrameworkCore;
using MVC.Intro.Models;

namespace MVC.Intro.Data
{
    public class AppDbContext : DbContext
    {
        public string DbPath { get; }

        public AppDbContext() : base(new DbContextOptions<AppDbContext>())
        {
            var projectRoot = Directory.GetCurrentDirectory();
            var dataFolder = Path.Combine(projectRoot, "Data");
            Directory.CreateDirectory(dataFolder);
            DbPath = Path.Combine(dataFolder, "products.db");
        }

        public DbSet<Product> Products { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlite($"Data Source={DbPath}");
            }                
        }
            
    }
}
