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
        Task AddPostmarkAsync(long postId, PostMark mark);

        Task<bool> IsPostExists(long postId);
    }
}