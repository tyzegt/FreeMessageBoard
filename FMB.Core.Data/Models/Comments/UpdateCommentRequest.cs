using FMB.Core.Data.Models.BaseTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#nullable disable

namespace FMB.Core.Data.Models.Comments
{
    public class UpdateCommentRequest : IEntity<long>
    {
        public string NewBody { get; set; }
    }
}