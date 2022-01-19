using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace ServerAPI.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var categorySeed = new Category[3];

            for (int i = 1; i < categorySeed.Length + 1; i++)
            {
                categorySeed[i - 1] = new Category
                {
                    CategoryId = i,
                    ThumbnailPath = "uploads/placeholder.jpg",
                    Name = $"Category {i}",
                    Description = $"This is a description of category {i}"
                };
            }

            modelBuilder.Entity<Category>().HasData(categorySeed);
        }
    }
}
