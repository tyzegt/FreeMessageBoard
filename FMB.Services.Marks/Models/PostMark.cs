using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMB.Services.Marks.Models
{
    public class PostMark
    {
        public long UserId { get; set; }
        public long PostId { get; set; }
        public MarkEnum Mark { get; set; }
    }
}
