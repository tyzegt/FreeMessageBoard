using FMB.Core.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMB.Core.Data.Models.Marks
{
    public class RatingDto
    {
        public long EntityId { get; set; }
        public RateableEntityType Type { get; set; }
        public int UpVotes { get; set; }
        public int DownVotes { get; set; }
        public int Rating { 
            get 
            {
                return UpVotes - DownVotes;
            } 
        }
    }
}
