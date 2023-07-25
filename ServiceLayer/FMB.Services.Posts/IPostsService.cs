using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMB.Services.Posts
{
    public interface IPostsService
    {
        Task CreatePostAsync(string title, string body);
        Task DeletePostAsync(long postId);
        Task<Post> GetPostAsync(long postId);
        Task UpdatePostAsync(long postId, string newPostBody, string? newPostTitle);
        Task<IEnumerable<Post>> GetAllPostsAsync(); 
    }
}