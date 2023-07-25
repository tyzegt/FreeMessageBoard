using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMB.Core.Models
{
    public class Post
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; }
        public User Author { get; set; }
        public string Label { get; set; }
        public string Body { get; set; }
    }
    public class PostMark
    {
        public Guid UserId { get; set; }
        public Guid PostId { get; set; }
        public string Mark { get; set; }
    }
}