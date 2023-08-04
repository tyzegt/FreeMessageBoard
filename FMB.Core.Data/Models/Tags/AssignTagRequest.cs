using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMB.Core.Data.Models.Tags
{
    public class AssignTagRequest
    {
        public long PostId { get; set; }
        public long TagId { get; set; }
    }
}
