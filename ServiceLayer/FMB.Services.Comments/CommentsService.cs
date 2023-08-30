using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using FMB.Services.Comments.Models;
using Microsoft.EntityFrameworkCore;

namespace FMB.Services.Comments
{
    public class CommentsService : ICommentsService
    {
        private readonly CommentsContext _context;
        public CommentsService(CommentsContext context)
        {
            _context = context;
        }
        public async Task<long> CreateComment(long postId, long parentCommentId, string body, long userId)
        {
            var comment = new Comment
            {
                PostId = postId,
                ParentCommentId = parentCommentId,
                Body = body,
                CreatedAt = DateTime.UtcNow,
                UserId = userId
            };
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment.Id;
        }

        public async Task DeleteComment(long commentId)
        {
            var targetComment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == commentId);
            if (targetComment != null) _context.Remove(targetComment);
            await _context.SaveChangesAsync();
        }

        public async Task<Comment?> GetComment(long commentId)
            => await _context.Comments.FirstOrDefaultAsync(c => c.Id == commentId); //Передача ошибок через Exception дорогая

        public async Task<IEnumerable<Comment>> GetCommentsByParentCommentId(long parentCommentId)
        {
            return await _context.Comments
                .Where(c => c.ParentCommentId == parentCommentId)
                .ToListAsync(); 
        }

        public async Task<IEnumerable<Comment>> GetCommentsByPostId(long postId)
        {
            return await _context.Comments
                .Where(c =>c.PostId == postId)
                .ToListAsync();
        }

        public async Task UpdateComment(long commentId, string newCommentBody)
        {
            if(string.IsNullOrEmpty(newCommentBody)) throw new ArgumentNullException(nameof(newCommentBody));
            
            var targetComment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == commentId);
            if (targetComment == null) throw new Exception($"comment {commentId} not found");
            
            targetComment.Body = newCommentBody;
            _context.Comments.Update(targetComment);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsCommentExists(long commentId, long postId)
            => await _context.Comments.AnyAsync(x => x.PostId == postId && x.Id == commentId);

        public async Task<bool> IsCommentExists(long commentId)
            => await _context.Comments.AnyAsync(x => x.Id == commentId);

        public async Task<bool> IsCommentsExists(params long[] commentIds)
            => (await _context.Comments.Where(x => commentIds.Contains(x.Id)).CountAsync()) == commentIds.Length;
        
    }
}