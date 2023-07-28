using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#nullable disable

namespace FMB.Services.Comments.Models
{
    public class Comment
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public long UserId { get; set; }
        public long ParentCommentId { get; set; }
        public string Body { get; set; }
        public long PostId { get; set; }
    }
}