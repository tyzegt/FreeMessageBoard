using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMB.Core.Models
{
    public class Comment
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; }
        public User Author { get; set; } 
        public string Body { get; set; }
    }
    public class CommentMark
    {
        public Guid UserId { get; set; }
        public Guid CommentId { get; set; }
        public string Mark { get; set; }
    }
}