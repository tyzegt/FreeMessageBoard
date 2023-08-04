using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMB.Services.Tags.Models
{
    public class PostTag
    {
        public long PostId { get; set; }
        public long TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
