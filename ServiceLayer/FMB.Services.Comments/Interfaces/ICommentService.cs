using FMB.Services.Comments.Models.Entities;

namespace FMB.Services.Comments.Interfaces;
public interface ICommentService
{
    Task<int> Add(Comment comment);
    Task Update(int commentId, string body);
    Task Delete(int commentId);
    Task Mark(int commentId, int userId, string mark);
}
