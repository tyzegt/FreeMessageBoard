using FMB.Services.Comments.Interfaces;
using FMB.Services.Comments.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FMB.Services.Comments;
public class ApplicationCommentsDbContext : DbContext, IApplicationCommentsDbContext
{
    public ApplicationCommentsDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Comment> Comments => Set<Comment>();
    public DbSet<CommentMark> CommentMarks => Set<CommentMark>();
}
