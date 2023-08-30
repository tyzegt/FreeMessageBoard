using FMB.Core.Data.Models.Marks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMB.Core.Data.Models.Comments
{
    public class CommentDto
    {
        public long CommentId { get; set; }
        public DateTime CreatedAt { get; set; }
        public RatingDto Rating { get; set; }
        public string Body { get; set; }
        public IReadOnlyCollection<CommentDto> ChildComments { get; set; }
        public long AuthorId { get; set; }
        public string AuthorName { get; set; }
    }
}
