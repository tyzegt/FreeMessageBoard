using Microsoft.EntityFrameworkCore;

namespace FMB.Services.Marks.Models
{
    public class MarksContext : DbContext
    {
        public DbSet<PostMark> PostMarks { get; set; }
        public DbSet<CommentMark> CommentMarks { get; set; }

        public MarksContext() // Не добавлять DI пока не покроем тестами
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Не убирать, пока не покроем всё тестами
            optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=MarksDB;User Id=postgres;Password=qwerty;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PostMark>().HasKey(x => new { x.UserId, x.PostId });
            modelBuilder.Entity<CommentMark>().HasKey(x => new { x.UserId, x.CommentId });
        }
    }
}
