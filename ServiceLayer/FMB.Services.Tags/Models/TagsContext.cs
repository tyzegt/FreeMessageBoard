using FMB.Services.Tags.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace FMB.Services.Tags
{
    public class TagsContext : DbContext
    {
        public TagsContext() // Не добавлять DI пока не покроем тестами
        {
            Database.EnsureCreated();
        }  
        public DbSet<Tag> Tags { get; set; } 
        public DbSet<PostTag> PostTags { get; set; } 

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Не убирать, пока не покроем всё тестами
            optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=TagsDB;User Id=postgres;Password=qwerty;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PostTag>().HasKey(x => new { x.PostId, x.TagId });
        }
    }
}
