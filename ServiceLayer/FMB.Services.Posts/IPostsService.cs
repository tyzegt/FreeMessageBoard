using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FMB.Services.Posts.Models;

namespace FMB.Services.Posts
{
    public interface IPostsService
    {
        Task<long> CreatePost(string title, string body, long userId);
        Task DeletePost(long postId);
        Task<Post> GetPost(long postId);
        Task UpdatePost(long postId, string newPostBody, string? newPostTitle);
        Task<bool> IsPostExists(long postId);
    }
}