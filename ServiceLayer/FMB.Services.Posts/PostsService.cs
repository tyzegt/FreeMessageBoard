using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FMB.Services.Posts
{
    public class PostsService : IPostsService
    {
        private readonly PostsContext _context;
        public PostsService(PostsContext context) 
        {
            _context = context;
        }
        public async Task CreatePostAsync(string title, string body) // TODO ��������� �����
        {
            if(string.IsNullOrEmpty(title)) { throw new ArgumentNullException("title"); }
            if(string.IsNullOrEmpty(body)) { throw new ArgumentNullException("body"); }

            await _context.Posts.AddAsync(new Post()
            {
                Label = title,
                Body = body,
                CreatedAt = DateTime.Now,
                Author = "TBD" // TODO current user id
            });
            await _context.SaveChangesAsync();
        }

        public async Task DeletePostAsync(long postId)
        {
            var targetPost = await _context.Posts.FirstOrDefaultAsync(p => p.Id == postId);
            if (targetPost == null) throw new Exception($"post {postId} not found");

            _context.Remove(targetPost);
            await _context.SaveChangesAsync();
            
        }

        public async Task<Post> GetPostAsync(long postId)
        {
            var targetPost = await _context.Posts
                .Include(p => p.PostMarks)
                .FirstOrDefaultAsync(p => p.Id == postId);

            return targetPost?? throw new Exception("Post doesn't exist");
        }
 

        public async Task UpdatePostAsync(long postId, string newPostBody, string? newPostTitle)
        {
            if(string.IsNullOrEmpty(newPostBody)) { throw new ArgumentNullException("newPostBody"); }
            if(string.IsNullOrEmpty(newPostTitle)) { throw new ArgumentNullException("newPostTitle"); }

            var targetPost = await _context.Posts.FirstOrDefaultAsync(p => p.Id == postId);
            if (targetPost == null) throw new Exception($"post {postId} not found");
            
            targetPost.Body = newPostBody;
            targetPost.Title = string.IsNullOrEmpty(newPostTitle) ? targetPost.Title : newPostTitle;
            _context.Posts.Update(targetPost);
            await _context.SaveChangesAsync();
        }

        public async Task AddPostmarkAsync(long postId, PostMark mark)
        {
            var targetPost = _context.Posts.FirstOrDefault(p => p.Id == postId);
            if (targetPost == null) throw new Exception($"post {postId} not found");
            
            targetPost.PostMarks.Add(mark);
            _context.Posts.Update(targetPost);
            await _context.SaveChangesAsync();
        }
    }
}