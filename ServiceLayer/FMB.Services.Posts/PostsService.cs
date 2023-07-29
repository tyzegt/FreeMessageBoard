using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FMB.Services.Posts.Models;
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
        public async Task<long> CreatePostAsync(string title, string body, long userId) 
        {
            if(string.IsNullOrEmpty(title)) { throw new ArgumentNullException(nameof(title)); }
            if(string.IsNullOrEmpty(body)) { throw new ArgumentNullException(nameof(body)); }

            var post = new Post()
            {
                Title = title,
                Body = body,
                CreatedAt = DateTime.UtcNow,
                UserId = userId
            };

            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
            return post.Id;
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
                .FirstOrDefaultAsync(p => p.Id == postId);

            return targetPost?? throw new Exception("Post doesn't exist");
        }
 

        public async Task UpdatePostAsync(long postId, string newPostBody, string? newPostTitle)
        {
            if(string.IsNullOrEmpty(newPostBody)) { throw new ArgumentNullException(nameof(newPostBody)); }
            if(string.IsNullOrEmpty(newPostTitle)) { throw new ArgumentNullException(nameof(newPostTitle)); }

            var targetPost = await _context.Posts.FirstOrDefaultAsync(p => p.Id == postId);
            if (targetPost == null) throw new Exception($"post {postId} not found");
            
            targetPost.Body = newPostBody;
            targetPost.Title = string.IsNullOrEmpty(newPostTitle) ? targetPost.Title : newPostTitle;
            _context.Posts.Update(targetPost);
            await _context.SaveChangesAsync();
        }

        public async Task RatePostAsync(long postId, PostMarks newMark, long userId)
        {
            var targetPost = _context.Posts.FirstOrDefault(p => p.Id == postId);
            if (targetPost == null) throw new Exception($"post {postId} not found");

            var mark = _context.PostMarks.FirstOrDefault(p => p.UserId == userId && p.PostId == postId);
            if (mark == null)
            {
                mark = new PostMark { PostId = postId, UserId = userId, Mark = newMark };
                _context.PostMarks.Add(mark);
            }
            else if (mark.Mark == newMark)
            {
                _context.Remove(mark);
            } else
            {
                mark.Mark = newMark;
            }

            await _context.SaveChangesAsync();
        }


        /// <summary>
        /// Проверяет существует ли пост с таким Id
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public Task<bool> IsPostExists(long postId)
            => _context.Posts.AnyAsync(x => x.Id == postId);
    }
}