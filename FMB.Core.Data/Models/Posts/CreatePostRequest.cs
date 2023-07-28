#nullable disable

using FMB;

namespace FMB.Core.Data.Models.Posts
{
    public class CreatePostRequest
    {
        public string Title { get; set; }
        public string Body { get; set; }
    }
}