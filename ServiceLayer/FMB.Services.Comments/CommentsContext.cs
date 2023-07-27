using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


#nullable disable

namespace FMB.Services.Comments
{
    public class CommentsContext : DbContext
    {
        IConfiguration _configuration;
        public CommentsContext(DbContextOptions<CommentsContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
            Database.EnsureCreated();
        }  
        public DbSet<Comment> Comments { get; set; } 

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {  
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("CommentsContext")); 
        }
    }
}