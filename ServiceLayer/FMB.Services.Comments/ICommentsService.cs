using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FMB.Services.Comments.Models;

namespace FMB.Services.Comments
{
    public interface ICommentsService
    {
        Task<long> CreateComment(long postId, long parentCommentId, string body, long userId);
        Task DeleteComment(long commentId);
        Task<Comment?> GetComment(long commentId);
        Task UpdateComment(long commentId, string newCommentBody);
        Task<IEnumerable<Comment>> GetCommentsByPostId(long postId);
        Task<IEnumerable<Comment>> GetCommentsByParentCommentId(long parentCommentId);
        Task<bool> IsCommentExists(long commentId, long postId);
        Task<bool> IsCommentExists(long commentId);
    }
}