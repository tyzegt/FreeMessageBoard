using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMB.Services.Posts.Models
{
    public class PostMark
    {
        public long UserId { get; set; }
        public long PostId { get; set; }
        public PostMarks Mark { get; set; }
    }

    public enum PostMarks
    {
        Minus = 0,
        Plus = 1
    }
}
