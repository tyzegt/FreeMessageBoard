using FMB.Services.Comments.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FMB.Services.Comments.Interfaces;
public interface IApplicationCommentsDbContext
{
    DbSet<Comment> Comments { get; }
    DbSet<CommentMark> CommentMarks { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
