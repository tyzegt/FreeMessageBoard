using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FMB.Services.Posts
{
    public class PostsService : IPostsService
    {

        public PostsService() // TODO DI
        {

        }
        public async Task CreatePostAsync(string title, string body) // TODO валидация строк
        {
            if(string.IsNullOrEmpty(title)) { throw new ArgumentNullException("title"); }
            if(string.IsNullOrEmpty(body)) { throw new ArgumentNullException("body"); }

            using (var _context = new PostsContext())
            {
                await _context.Posts.AddAsync(new Post()
                {
                    Title = title,
                    Body = body,
                    CreatedAt = DateTime.Now,
                    Author = "TBD" // TODO current user id
                });
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeletePostAsync(long postId)
        {
            using (var _context = new PostsContext())
            {
                var targetPost = await _context.Posts.FirstOrDefaultAsync(p => p.Id == postId);
                if (targetPost != null)
                {
                    _context.Remove(targetPost);
                    await _context.SaveChangesAsync();
                }
            }
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            using (var _context = new PostsContext())
            {
                return await _context.Posts.ToListAsync();
            }
        }

        public async Task<Post> GetPostAsync(long postId)
        {
            using (var _context = new PostsContext())
            {
                var targetPost = await _context.Posts.FirstOrDefaultAsync(p => p.Id == postId);

                return targetPost ?? new Post { Title = "Post doesn't exist" };
            }
        }
 

        public async Task UpdatePostAsync(long postId, string newPostBody, string? newPostTitle)
        {
            using (var _context = new PostsContext())
            {
                var targetPost = await _context.Posts.FirstOrDefaultAsync(p => p.Id == postId);
                if (targetPost != null)
                {
                    targetPost.Body = newPostBody;
                    targetPost.Title = string.IsNullOrEmpty(newPostTitle) ? targetPost.Title : newPostTitle;
                    _context.Posts.Update(targetPost);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}