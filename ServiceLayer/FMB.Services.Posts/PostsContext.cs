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
        IConfiguration _configuration;
        public PostsContext(DbContextOptions<PostsContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
            Database.EnsureCreated();
        }  
        public DbSet<Post> Posts { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { 
            // TODO: вынести в конфиги
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("PostsContext")); 
        } 
    }
    
}