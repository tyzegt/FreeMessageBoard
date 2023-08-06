using FMB.Core.Data.Models.BaseTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMB.Core.Data.Models.Posts
{
    public class RateRequest : IEntity<long>
    {
        public bool Upvote { get; set; }
    }
}
