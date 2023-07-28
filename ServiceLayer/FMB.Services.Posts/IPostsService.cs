using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FMB.Services.Posts.Models;

namespace FMB.Services.Posts
{
    public interface IPostsService
    {
        Task<long> CreatePostAsync(string title, string body, long userId);
        Task DeletePostAsync(long postId);
        Task<Post> GetPostAsync(long postId);
        Task UpdatePostAsync(long postId, string newPostBody, string? newPostTitle);
        Task<bool> IsPostExists(long postId);
        Task RatePostAsync(long postId, PostMarks newMark, long userId);
    }
}