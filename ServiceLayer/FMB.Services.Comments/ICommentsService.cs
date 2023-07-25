using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMB.Services.Comments
{
    public interface ICommentsService
    {
        Task CreateCommentAsync(Comment comment);
        Task DeleteCommentAsync(long commentId);
        Task<Comment> GetCommentAsync(long commentId);
        Task UpdateCommentAsync(long commentId, string newCommentBody);
        Task<IEnumerable<Comment>> GetAllCommentsByPostIdAsync(long postId);

        Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(long postId);
        Task<IEnumerable<Comment>> GetCommentsByParentCommentIdAsync(long parentCommentId);
    }
}