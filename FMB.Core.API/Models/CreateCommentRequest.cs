using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FMB.Services.Comments;

#nullable disable

namespace FMB.Core.API.Models
{
    public class CreateCommentRequest
    {
        public Comment Comment { get; set; }
    }
}