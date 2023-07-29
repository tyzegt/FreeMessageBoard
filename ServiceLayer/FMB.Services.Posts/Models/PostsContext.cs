using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace FMB.Services.Posts.Models
{
    public class PostsContext : DbContext
    {
        public PostsContext() // Не добавлять DI пока не покроем тестами
        {
            Database.EnsureCreated();
        }
        public DbSet<Post> Posts { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Не убирать, пока не покроем всё тестами
            optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=PostsDB;User Id=postgres;Password=qwerty;");
        }
    }

}