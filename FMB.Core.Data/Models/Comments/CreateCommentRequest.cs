using FMB.Core.Data.Models.BaseTypes;

namespace FMB.Core.Data.Models.Comments
{
    public class CreateCommentRequest : IEntity<long>
    {
        public long ParentCommentId { get; set; }
        public string Body { get; set; }
    }
}