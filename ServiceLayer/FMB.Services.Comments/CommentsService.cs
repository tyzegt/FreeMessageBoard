using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task CreateCommentAsync(long postId, long parentCommentId, string body)
    {
            await _context.Comments.AddAsync(new Comment
            {
                PostId = postId,
                ParentCommentId = parentCommentId,
                Body = body,
                CreatedAt = DateTime.UtcNow,
                UserId = 123 // TODO current user
            });
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCommentAsync(long commentId)
        {
            var targetComment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == commentId);
            if (targetComment != null) _context.Remove(targetComment);
            await _context.SaveChangesAsync();
        }

        public async Task<Comment> GetCommentAsync(long commentId)
        {
            var targetComment =  await _context.Comments
                .FirstOrDefaultAsync(c => c.Id == commentId);
            
            return targetComment?? new Comment();
        }

        public async Task<IEnumerable<Comment>> GetCommentsByParentCommentIdAsync(long parentCommentId)
        {
            return await _context.Comments
                .Where(c => c.ParentCommentId == parentCommentId)
                .ToListAsync(); 
        }

        public async Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(long postId)
        {
            return await _context.Comments
                .Where(c =>c.PostId == postId)
                .ToListAsync();
        }

        public async Task UpdateCommentAsync(long commentId, string newCommentBody)
        {
            if(string.IsNullOrEmpty(newCommentBody)) throw new ArgumentNullException("newCommentBody");
            
            var targetComment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == commentId);
            if (targetComment != null)
            {
                targetComment.Body = newCommentBody;
                _context.Comments.Update(targetComment);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Comment>> GetAllCommentsByPostIdAsync(long postId)
        {
            return await _context.Comments.Where(c => c.PostId == postId).ToListAsync();
        }

        public async Task AddCommentMarkAsync(long commentId, CommentMark mark)
        {
            var targetComment = _context.Comments.FirstOrDefault(c => c.Id == commentId);
            if(targetComment != null )
            {
                targetComment.CommentMarks.Add(mark);
                _context.Update(targetComment);
                await _context.SaveChangesAsync();
            }
        }
    }
}