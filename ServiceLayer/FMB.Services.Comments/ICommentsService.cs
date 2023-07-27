using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMB.Services.Comments
{
    public interface ICommentsService
    {
        Task CreateCommentAsync(long postId, long parentCommentId, string body);
        Task DeleteCommentAsync(long commentId);
        Task<Comment> GetCommentAsync(long commentId);
        Task UpdateCommentAsync(long commentId, string newCommentBody);
        Task<IEnumerable<Comment>> GetAllCommentsByPostIdAsync(long postId);

        Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(long postId);
        Task<IEnumerable<Comment>> GetCommentsByParentCommentIdAsync(long parentCommentId);
        Task AddCommentMarkAsync(long commentId, CommentMark mark);
    }
}