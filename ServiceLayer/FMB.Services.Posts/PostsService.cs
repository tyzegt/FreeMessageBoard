using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FMB.Services.Posts
{
    public class PostsService : IPostsService
    {
        PostsContext _context;
        public PostsService(PostsContext context)
        {
            _context = context;
        }
        public async Task CreatePostAsync(Post post)
        {
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePostAsync(long postId)
        {
            var targetPost = await _context.Posts.FirstOrDefaultAsync(p => p.Id == postId);
            if(targetPost != null)
            {
                _context.Remove(targetPost);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            return await _context.Posts.ToListAsync();
        }

        public async Task<Post> GetPostAsync(long postId)
        {
            var targetPost = await _context.Posts.FirstOrDefaultAsync(p => p.Id == postId);

            return targetPost?? new Post{ Label = "Post doesn't exist"};
        }
 

        public async Task UpdatePostAsync(long postId, string newPostBody, string? newPostTitle)
        {
            var targetPost = await _context.Posts.FirstOrDefaultAsync(p => p.Id == postId);
            if(targetPost != null)
            {
                targetPost.Body = newPostBody;
                targetPost.Label = string.IsNullOrEmpty(newPostTitle)? targetPost.Label : newPostTitle;
                _context.Posts.Update(targetPost);
                await _context.SaveChangesAsync();
            }
        }
    }
}