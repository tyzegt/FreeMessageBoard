using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMB.Services.Posts
{
    public interface IPostsService
    {
        Task CreatePostAsync(Post post);
        Task DeletePostAsync(long postId);
        Task<Post> GetPostAsync(long postId);
        Task UpdatePostAsync(long postId, string newPostBody, string? newPostTitle);
        Task<IEnumerable<Post>> GetAllPostsAsync(); 
        Task AddPostmarkAsync(long postId, PostMark mark);
    }
}