using Microsoft.EntityFrameworkCore;

namespace FMB.Services.Marks.Models
{
    public class MarksContext : DbContext
    {
        public DbSet<PostMark> PostMarks { get; set; }
        public DbSet<CommentMark> CommentMarks { get; set; }

        public MarksContext(DbContextOptions<MarksContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PostMark>().HasKey(x => new { x.UserId, x.PostId });
            modelBuilder.Entity<CommentMark>().HasKey(x => new { x.UserId, x.CommentId });
        }
    }
}
