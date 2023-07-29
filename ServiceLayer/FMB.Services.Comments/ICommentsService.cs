using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FMB.Services.Comments.Models;

namespace FMB.Services.Comments
{
    public interface ICommentsService
    {
        Task<long> CreateCommentAsync(long postId, long parentCommentId, string body, long userId);
        Task DeleteCommentAsync(long commentId);
        Task<Comment?> GetCommentAsync(long commentId);
        Task UpdateCommentAsync(long commentId, string newCommentBody);
        Task<IEnumerable<Comment>> GetAllCommentsByPostIdAsync(long postId);

        Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(long postId);
        Task<IEnumerable<Comment>> GetCommentsByParentCommentIdAsync(long parentCommentId);
        Task<bool> IsCommentExists(long commentId, long postId);
    }
}