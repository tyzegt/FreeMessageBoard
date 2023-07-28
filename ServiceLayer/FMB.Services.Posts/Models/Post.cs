using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#nullable disable

namespace FMB.Services.Posts.Models
{
    public class Post
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public long UserId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}