using FMB.Services.Tags.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace FMB.Services.Tags
{
    public class TagsContext : DbContext
    {
        public DbSet<Tag> Tags { get; set; } 
        public DbSet<PostTag> PostTags { get; set; }

        public TagsContext(DbContextOptions<TagsContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PostTag>().HasKey(x => new { x.PostId, x.TagId });
        }
    }
}
