using FMB.Services.Tags.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace FMB.Services.Tags
{
    public class TagsContext : DbContext
    {
        IConfiguration _configuration;
        public TagsContext(DbContextOptions<TagsContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
            Database.EnsureCreated();
        }  
        public DbSet<Tag> Tags { get; set; } 

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {  
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("TagsContext")); 
        }
    }
}
