using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMB.Services.Comments.Models
{
    public class CommentMark
    {
        public long UserId { get; set; }
        public long CommentId { get; set; }
        public CommentMarks Mark { get; set; }
    }

    public enum CommentMarks
    {
        Minus = 0,
        Plus = 1
    }
}
