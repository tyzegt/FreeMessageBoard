using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace FMB.Services.Posts
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