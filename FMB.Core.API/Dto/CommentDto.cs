using FMB.Core.Data.Models.Marks;
using FMB.Services.Comments.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMB.Core.API.Dto
{
    public class CommentDto
    {
        public long CommentId { get; set; }
        public long ParentCommentId { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UpVotes { get; set; }
        public int DownVotes { get; set; }
        public string Body { get; set; }
        public IReadOnlyCollection<CommentDto> ChildComments { get; set; }
        public long AuthorId { get; set; }
        public string AuthorName { get; set; }

        public static List<CommentDto> GetCommentsTree (List<Comment> comments, List<RatingDto> ratings)
        {
            var dtos = new List<CommentDto> ();
            foreach (var comment in comments)
            {
                dtos.Add(MapFrom(comment, ratings.FirstOrDefault(x => x.EntityId == comment.Id)));
            }
            var result = new List<CommentDto> ();
            foreach (var comment in dtos) { 
                comment.ChildComments = dtos.Where(x => x.ParentCommentId == comment.CommentId).ToList();
                if(comment.ParentCommentId == 0) { result.Add(comment); }
            }

            return result;
        }
    
        public static CommentDto MapFrom(Comment comment, RatingDto rating)
        {
            return new CommentDto
            {
                CommentId = comment.Id,
                ParentCommentId = comment.ParentCommentId,
                CreatedAt = comment.CreatedAt,
                UpVotes = rating.UpVotes,
                DownVotes = rating.DownVotes,
                Body = comment.Body,
                AuthorId = comment.UserId,
                ChildComments = new List<CommentDto>(),
                AuthorName = "TBD" // TODO
            };
        }
    }

}
