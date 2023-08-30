using FMB.Core.Data.Models.Marks;
using FMB.Services.Marks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMB.Services.Marks
{
    public interface IMarksService
    {
        Task<RatingDto> GetCommentRating(long commentId);
        Task<List<RatingDto>> GetCommentsRating(List<long> commentIds);
        Task<RatingDto> GetPostRating(long postId);
        Task RateComment(long commentId, MarkEnum newMark, long userId);
        Task RatePost(long postId, MarkEnum newMark, long userId);
    }
}
