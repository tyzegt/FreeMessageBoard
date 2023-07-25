using FMB.Services.Comments.Interfaces;
using FMB.Services.Comments.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FMB.Services.Comments.Services;
public class CommentService : ICommentService
{
    private readonly IApplicationCommentsDbContext context;
    public CommentService(IApplicationCommentsDbContext context)
    {
        this.context = context;
    }

    public async Task<int> Add(Comment comment)
    {
        context.Comments.Add(comment);
        await context.SaveChangesAsync();
        return comment.Id;
    }

    public async Task Delete(int commentId) => await context.Comments.Where(c => c.Id == commentId).ExecuteDeleteAsync();

    public async Task Mark(int commentId, int userId, string mark)
    {
        var markEntity = await context.CommentMarks.FirstOrDefaultAsync(c => c.CommentId == commentId && c.UserId == userId);

        if (markEntity is not null)
            markEntity.Mark = mark;
        else context.CommentMarks.Add(new CommentMark
        {
            CommentId = commentId,
            UserId = userId,
            Mark = mark
        });

        await context.SaveChangesAsync();
    }

    public async Task Update(int commentId, string body)
    {
        var comment = await context.Comments.FirstOrDefaultAsync(c => c.Id == commentId);
        if (comment is not null)
        {
            comment.Body = body;
            await context.SaveChangesAsync();
        }
    }
}
