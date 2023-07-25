using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace FMB.Services.Posts
{
    public class PostsContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public PostsContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // TODO: вынести в конфиги
            optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=TagsDB;User Id=postgres;Password=qwerty;");
        }
    }
    
}