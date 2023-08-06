using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace FMB.Services.Posts.Models
{
    public class PostsContext : DbContext
    {
        public PostsContext(DbContextOptions<PostsContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Post> Posts { get; set; }
    }

}