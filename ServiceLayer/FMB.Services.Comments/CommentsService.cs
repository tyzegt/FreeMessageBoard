using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FMB.Services.Comments
{
    public class CommentsService : ICommentsService
    {
        public CommentsService() // TODO DI
        {

        }
        public async Task CreateCommentAsync(Comment comment)
        {
            using (var _context = new CommentsContext())
            {
                await _context.Comments.AddAsync(comment);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteCommentAsync(long commentId)
        {
            using (var _context = new CommentsContext())
            {
                var targetComment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == commentId);
                if (targetComment != null) _context.Remove(targetComment);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Comment> GetCommentAsync(long commentId)
        {
            using (var _context = new CommentsContext())
            {
                var targetComment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == commentId);

                return targetComment ?? new Comment();
            }
        }

        public async Task<IEnumerable<Comment>> GetCommentsByParentCommentIdAsync(long parentCommentId)
        {
            using (var _context = new CommentsContext())
            {
                return await _context.Comments.Where(c => c.ParentCommentId == parentCommentId).ToListAsync();
            }
        }

        public Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(long postId)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateCommentAsync(long commentId, string newCommentBody)
        {
            using (var _context = new CommentsContext())
            {
                var targetComment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == commentId);
                if (targetComment != null)
                {
                    targetComment.Body = newCommentBody;
                    _context.Comments.Update(targetComment);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}