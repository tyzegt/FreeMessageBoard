using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMB.Services.Marks.Models
{
    public class CommentMark
    {
        public long UserId { get; set; }
        public long CommentId { get; set; }
        public MarkEnum Mark { get; set; }
    }


}
